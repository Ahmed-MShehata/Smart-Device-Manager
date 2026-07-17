using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Orders.GetOrder;

/// <summary>
/// Handles <see cref="GetOrderQuery"/>.
/// Projects the full order, including all line items with product names, directly in SQL.
/// Uses <see cref="IReadDbContext"/> for zero-tracking, read-only access.
/// </summary>
public sealed class GetOrderHandler : IQueryHandler<GetOrderQuery, GetOrderResponse>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetOrderHandler"/>.</summary>
    public GetOrderHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<GetOrderResponse>> Handle(
        GetOrderQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _db.Orders
            .Where(o => o.Id == query.Id)
            .Select(o => new GetOrderResponse
            {
                Id           = o.Id,
                CustomerName = o.CustomerName,
                PhoneNumber  = o.PhoneNumber,
                Address      = o.Address,
                DeviceId     = o.Device.DeviceId,
                Status       = o.Status,
                Notes        = o.Notes,
                TotalPrice   = o.Items.Sum(i => i.Price * i.Quantity),
                CreatedAt    = o.CreatedAt,
                UpdatedAt    = o.UpdatedAt,
                CreatedBy    = o.CreatedBy,
                Items        = o.Items.Select(i => new OrderItemResponse
                {
                    Id          = i.Id,
                    ProductId   = i.ProductId,
                    ProductName = i.Product != null ? i.Product.Name : string.Empty,
                    Quantity    = i.Quantity,
                    Price       = i.Price,
                    LineTotal   = i.Price * i.Quantity
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<GetOrderResponse>.Failure(Error.NotFound("Order"));

        return Result<GetOrderResponse>.Success(response);
    }
}
