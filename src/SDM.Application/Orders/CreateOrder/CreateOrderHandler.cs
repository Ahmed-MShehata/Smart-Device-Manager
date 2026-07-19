using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;
using SDM.Domain.Enums;

namespace SDM.Application.Orders.CreateOrder;

/// <summary>
/// Handles the <see cref="CreateOrderCommand"/>.
/// Validates product availability, creates the Order aggregate,
/// attaches OrderItem children with price snapshots, reduces product stock,
/// and fires the SignalR OrderCreated event via <see cref="IOrderNotificationService"/>.
/// </summary>
public sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDb;
    private readonly IOrderNotificationService _notifications;

    /// <summary>Initializes a new instance of <see cref="CreateOrderHandler"/>.</summary>
    public CreateOrderHandler(
        IUnitOfWork unitOfWork,
        IReadDbContext readDb,
        IOrderNotificationService notifications)
    {
        _unitOfWork = unitOfWork;
        _readDb = readDb;
        _notifications = notifications;
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
        }

        // ─── 2. Build the Order aggregate ─────────────────────────────────────
        var order = new Order(
            command.CustomerName,
            command.CustomerPhone,
            command.CustomerWhatsApp,
            command.CustomerGovernorate,
            command.CustomerAddress);

        // ─── 3. Create order items with price snapshots ───────────────────────
        foreach (var lineItem in command.Items)
        {
            var product = productCache[lineItem.ProductId];
            var item = new OrderItem(order.Id, lineItem.ProductId, lineItem.Quantity, product.FinalPrice);
            order.AddItem(item);
            await _unitOfWork.OrderItems.AddAsync(item, cancellationToken);

            product.ReduceStock(lineItem.Quantity);
            _unitOfWork.Products.Update(product);
        }

        // ─── 4. Persist ───────────────────────────────────────────────────────
        await _unitOfWork.Orders.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // ─── 5. Notify admin via SignalR (fire-and-forget on failure) ─────────
        await _notifications.NotifyOrderCreatedAsync(
            order.Id,
            order.CustomerName,
            order.CreatedAt,
            order.Items.Count,
            cancellationToken);

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
