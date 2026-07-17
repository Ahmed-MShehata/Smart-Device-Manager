using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;
using SDM.Domain.Enums;
using SDM.Domain.ValueObjects;

namespace SDM.Application.Orders.CreateOrder;

/// <summary>
/// Handles the <see cref="CreateOrderCommand"/>.
/// Validates product availability and stock, creates the <see cref="Order"/> aggregate,
/// attaches <see cref="OrderItem"/> children with price snapshots, and reduces product stock.
/// </summary>
/// <remarks>
/// <para>
/// All products are loaded and validated before any mutation occurs.
/// If any product is missing, unavailable, or under-stocked the entire operation
/// fails before touching the database.
/// </para>
/// <para>
/// Because <see cref="BaseEntity"/> assigns <see cref="Guid.NewGuid"/> in its constructor,
/// <c>order.Id</c> is known immediately — no intermediary save is required before
/// creating <see cref="OrderItem"/> objects.
/// </para>
/// </remarks>
public sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDb;

    /// <summary>Initializes a new instance of <see cref="CreateOrderHandler"/>.</summary>
    public CreateOrderHandler(IUnitOfWork unitOfWork, IReadDbContext readDb)
    {
        _unitOfWork = unitOfWork;
        _readDb = readDb;
    }

    /// <inheritdoc/>
    public async Task<Result<CreateOrderResponse>> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        // ─── 1. Load and validate all products up-front ───────────────────────
        var productIds = command.Items.Select(i => i.ProductId).Distinct().ToList();
        
        var products = await _readDb.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var productCache = products.ToDictionary(p => p.Id);

        foreach (var lineItem in command.Items)
        {
            if (!productCache.TryGetValue(lineItem.ProductId, out var product))
                return Result<CreateOrderResponse>.Failure(
                    Error.NotFound($"Product ({lineItem.ProductId})"));

            if (product.Status != ProductStatus.Active)
                return Result<CreateOrderResponse>.Failure(new Error(
                    "Product.NotAvailable",
                    $"Product '{product.Name}' is not available for ordering."));

            if (product.Quantity < lineItem.Quantity)
                return Result<CreateOrderResponse>.Failure(new Error(
                    "Product.InsufficientStock",
                    $"Insufficient stock for '{product.Name}'. Available: {product.Quantity}, requested: {lineItem.Quantity}."));

            productCache[lineItem.ProductId] = product;
        }

        // ─── 2. Build the Order aggregate ─────────────────────────────────────
        var deviceRef = new DeviceReference(command.DeviceId);
        var order = new Order(command.CustomerName, command.PhoneNumber, command.Address, deviceRef);

        if (!string.IsNullOrWhiteSpace(command.Notes))
            order.SetNotes(command.Notes);

        // ─── 3. Create order items with price snapshots ───────────────────────
        // FinalPrice is used as the snapshot — captures discount at order time.
        // order.Id is already set (Guid.NewGuid() in BaseEntity constructor).
        foreach (var lineItem in command.Items)
        {
            var product = productCache[lineItem.ProductId];
            var item = new OrderItem(order.Id, lineItem.ProductId, lineItem.Quantity, product.FinalPrice);
            order.AddItem(item);
            await _unitOfWork.OrderItems.AddAsync(item, cancellationToken);

            // TODO: In future multi-user deployments, protect this stock update using optimistic concurrency 
            // (e.g., adding a RowVersion/ConcurrencyToken to the Product entity) to avoid race conditions.
            product.ReduceStock(lineItem.Quantity);
            _unitOfWork.Products.Update(product);
        }

        // ─── 4. Persist ───────────────────────────────────────────────────────
        await _unitOfWork.Orders.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateOrderResponse>.Success(
            new CreateOrderResponse
            {
                Id           = order.Id,
                CustomerName = order.CustomerName,
                Status       = order.Status,
                TotalPrice   = order.TotalPrice,
                ItemCount    = order.Items.Count,
                CreatedAt    = order.CreatedAt
            },
            "Order placed successfully.");
    }
}
