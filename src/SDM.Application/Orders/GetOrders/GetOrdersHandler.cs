using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Orders.GetOrders;

/// <summary>
/// Handles <see cref="GetOrdersQuery"/>.
/// Executes a fully server-side paginated, filtered, and sorted query
/// via <see cref="IReadDbContext"/>.
/// </summary>
/// <remarks>
/// The <c>TotalPrice</c> and <c>ItemCount</c> projections are translated to SQL aggregates.
/// No entities are loaded into memory. <c>AsNoTracking</c> is enforced by
/// the <see cref="IReadDbContext"/> implementation.
/// </remarks>
public sealed class GetOrdersHandler
    : IQueryHandler<GetOrdersQuery, PaginationResponse<GetOrdersResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetOrdersHandler"/>.</summary>
    public GetOrdersHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<PaginationResponse<GetOrdersResponse>>> Handle(
        GetOrdersQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.Orders.AsQueryable();

        // ─── Search ───────────────────────────────────────────────────────────
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(o =>
                EF.Functions.Like(o.CustomerName, $"%{query.Search}%") ||
                EF.Functions.Like(o.CustomerPhone,  $"%{query.Search}%"));
        }

        // ─── Filter ───────────────────────────────────────────────────────────
        if (query.Status.HasValue)
            q = q.Where(o => o.Status == query.Status.Value);

        // ─── Sort ─────────────────────────────────────────────────────────────
        q = (query.SortBy, query.Descending) switch
        {
            (OrderSortBy.CustomerName, false) => q.OrderBy(o => o.CustomerName),
            (OrderSortBy.CustomerName, true)  => q.OrderByDescending(o => o.CustomerName),
            (OrderSortBy.TotalPrice,   false) => q.OrderBy(o => o.Items.Sum(i => i.Price * i.Quantity)),
            (OrderSortBy.TotalPrice,   true)  => q.OrderByDescending(o => o.Items.Sum(i => i.Price * i.Quantity)),
            (_,                        false) => q.OrderBy(o => o.CreatedAt),
            _                                 => q.OrderByDescending(o => o.CreatedAt)
        };

        // ─── Count ────────────────────────────────────────────────────────────
        var totalCount = await q.CountAsync(cancellationToken);

        // ─── Project + Paginate ───────────────────────────────────────────────
        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(o => new GetOrdersResponse
            {
                Id           = o.Id,
                CustomerName = o.CustomerName,
                CustomerPhone = o.CustomerPhone,
                Status       = o.Status,
                TotalPrice   = o.Items.Sum(i => i.Price * i.Quantity),
                ItemCount    = o.Items.Count,
                CreatedAt    = o.CreatedAt
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response   = PaginationResponse<GetOrdersResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetOrdersResponse>>.Success(response);
    }
}
