using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Products.GetProducts;

/// <summary>
/// Handles the <see cref="GetProductsQuery"/>.
/// Executes a fully server-side paginated, filtered, sorted, and projected
/// SQL query via <see cref="IReadDbContext"/>.
/// </summary>
/// <remarks>
/// <para>
/// No entities are loaded into memory. The <c>Select</c> projection executes entirely
/// in SQL Server, including the <c>FinalPrice</c> computation.
/// </para>
/// <para>
/// <c>AsNoTracking</c> is enforced by the <see cref="IReadDbContext"/> implementation —
/// this handler performs zero change-tracking operations.
/// </para>
/// </remarks>
public sealed class GetProductsHandler
    : IQueryHandler<GetProductsQuery, PaginationResponse<GetProductsResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>
    /// Initializes a new instance of <see cref="GetProductsHandler"/>.
    /// </summary>
    /// <param name="db">
    /// Read-only database context. Provides <c>AsNoTracking</c> query sets
    /// for all entities.
    /// </param>
    public GetProductsHandler(IReadDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResponse<GetProductsResponse>>> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        // Start with the read-only, NoTracking product set
        var q = _db.Products;

        // ─── Search ───────────────────────────────────────────────────────────
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(p =>
                EF.Functions.Like(p.Name, $"%{query.Search}%") ||
                EF.Functions.Like(p.Brand, $"%{query.Search}%"));
        }

        // ─── Filter ───────────────────────────────────────────────────────────
        if (query.Status.HasValue)
            q = q.Where(p => p.Status == query.Status.Value);

        // ─── Sort ─────────────────────────────────────────────────────────────
        q = (query.SortBy, query.SortDirection) switch
        {
            (ProductSortBy.Name,      SortDirection.Ascending)  => q.OrderBy(p => p.Name),
            (ProductSortBy.Name,      SortDirection.Descending) => q.OrderByDescending(p => p.Name),
            (ProductSortBy.Price,     SortDirection.Ascending)  => q.OrderBy(p => p.Price),
            (ProductSortBy.Price,     SortDirection.Descending) => q.OrderByDescending(p => p.Price),
            (ProductSortBy.CreatedAt, SortDirection.Ascending)  => q.OrderBy(p => p.CreatedAt),
            (ProductSortBy.CreatedAt, SortDirection.Descending) => q.OrderByDescending(p => p.CreatedAt),
            _                                                    => q.OrderBy(p => p.Name)
        };

        // ─── Server-side COUNT (executes one SQL COUNT query) ─────────────────
        var totalCount = await q.CountAsync(cancellationToken);

        // ─── Project + Paginate (executes one SQL SELECT with OFFSET/FETCH) ───
        // Select() projects to DTO entirely in SQL — no entity graph is loaded.
        // FinalPrice = Price - (Price * Discount / 100) is translated to SQL arithmetic.
        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new GetProductsResponse
            {
                Id         = p.Id,
                Name       = p.Name,
                Brand      = p.Brand,
                Category   = p.Category,
                Price      = p.Price,
                Discount   = p.Discount,
                FinalPrice = p.Price - (p.Price * p.Discount / 100m),
                Quantity   = p.Quantity,
                ImagePath  = p.ImagePath,
                Status     = p.Status
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response = PaginationResponse<GetProductsResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetProductsResponse>>.Success(response);
    }
}
