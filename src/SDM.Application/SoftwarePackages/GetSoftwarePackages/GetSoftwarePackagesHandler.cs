using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>Handles <see cref="GetSoftwarePackagesQuery"/> with server-side pagination, filter, and sort.</summary>
public sealed class GetSoftwarePackagesHandler
    : IQueryHandler<GetSoftwarePackagesQuery, PaginationResponse<GetSoftwarePackagesResponse>>
{
    private readonly IReadDbContext _db;

    public GetSoftwarePackagesHandler(IReadDbContext db) => _db = db;

    public async Task<Result<PaginationResponse<GetSoftwarePackagesResponse>>> Handle(
        GetSoftwarePackagesQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.SoftwarePackages.AsQueryable();

        // Search by name or category
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(p =>
                EF.Functions.Like(p.Name, $"%{query.Search}%") ||
                EF.Functions.Like(p.Category, $"%{query.Search}%"));
        }

        // Filter by category (Application | Driver)
        if (!string.IsNullOrWhiteSpace(query.Category))
            q = q.Where(p => p.Category == query.Category);

        // Sort
        q = (query.SortBy, query.Descending) switch
        {
            (SoftwarePackageSortBy.Name,      false) => q.OrderBy(p => p.Name),
            (SoftwarePackageSortBy.Name,      true)  => q.OrderByDescending(p => p.Name),
            (SoftwarePackageSortBy.Category,  false) => q.OrderBy(p => p.Category),
            (SoftwarePackageSortBy.Category,  true)  => q.OrderByDescending(p => p.Category),
            (SoftwarePackageSortBy.CreatedAt, false) => q.OrderBy(p => p.CreatedAt),
            (SoftwarePackageSortBy.CreatedAt, true)  => q.OrderByDescending(p => p.CreatedAt),
            _                                        => q.OrderBy(p => p.Name)
        };

        var totalCount = await q.CountAsync(cancellationToken);

        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new GetSoftwarePackagesResponse
            {
                Id        = p.Id,
                Name      = p.Name,
                Category  = p.Category,
                Version   = p.Version,
                IconUrl   = p.IconUrl,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response   = PaginationResponse<GetSoftwarePackagesResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetSoftwarePackagesResponse>>.Success(response);
    }
}
