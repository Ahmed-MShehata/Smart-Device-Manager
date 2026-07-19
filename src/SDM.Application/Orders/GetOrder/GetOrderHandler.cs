using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Orders.GetOrder;

/// <summary>
/// Handles <see cref="GetOrderQuery"/>.
/// Projects the full order including all line items with product names in a single SQL query.
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
                Id                  = o.Id,
                CustomerName        = o.CustomerName,
                CustomerPhone       = o.CustomerPhone,
                CustomerWhatsApp    = o.CustomerWhatsApp,
                CustomerGovernorate = o.CustomerGovernorate,
                CustomerAddress     = o.CustomerAddress,
                Status              = o.Status,
                TotalPrice          = o.Items.Sum(i => i.Price * i.Quantity),
                CreatedAt           = o.CreatedAt,
                UpdatedAt           = o.UpdatedAt,
                Items               = o.Items.Select(i => new OrderItemResponse
                {
                    Id              = i.Id,
                    ProductId       = i.ProductId,
                    ProductName     = i.Product != null ? i.Product.Name : string.Empty,
                    ProductImageUrl = i.Product != null ? i.Product.ImagePath : null,
                    Quantity        = i.Quantity,
                    Price           = i.Price,
                    LineTotal       = i.Price * i.Quantity
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<GetOrderResponse>.Failure(Error.NotFound("Order"));

        return Result<GetOrderResponse>.Success(response);
    }
}
