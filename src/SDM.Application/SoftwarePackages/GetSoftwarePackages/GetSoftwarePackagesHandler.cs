using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>
/// Handles <see cref="GetSoftwarePackagesQuery"/> returning data via <see cref="IReadDbContext"/>.
/// </summary>
public sealed class GetSoftwarePackagesHandler : IQueryHandler<GetSoftwarePackagesQuery, PaginationResponse<GetSoftwarePackagesResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetSoftwarePackagesHandler"/>.</summary>
    public GetSoftwarePackagesHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<PaginationResponse<GetSoftwarePackagesResponse>>> Handle(
        GetSoftwarePackagesQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.SoftwarePackages.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(p =>
                EF.Functions.Like(p.Name, $"%{query.Search}%") ||
                EF.Functions.Like(p.Category, $"%{query.Search}%"));
        }

        if (query.Status.HasValue)
            q = q.Where(p => p.Status == query.Status.Value);

        if (query.InstallerType.HasValue)
            q = q.Where(p => p.InstallerType == query.InstallerType.Value);

        q = (query.SortBy, query.Descending) switch
        {
            (SoftwarePackageSortBy.Name, false)      => q.OrderBy(p => p.Name),
            (SoftwarePackageSortBy.Name, true)       => q.OrderByDescending(p => p.Name),
            (SoftwarePackageSortBy.Category, false)  => q.OrderBy(p => p.Category),
            (SoftwarePackageSortBy.Category, true)   => q.OrderByDescending(p => p.Category),
            (SoftwarePackageSortBy.Size, false)      => q.OrderBy(p => p.Size),
            (SoftwarePackageSortBy.Size, true)       => q.OrderByDescending(p => p.Size),
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
                Id            = p.Id,
                Name          = p.Name,
                Version       = p.Version,
                Category      = p.Category,
                Size          = p.Size,
                InstallerType = p.InstallerType,
                Status        = p.Status,
                CreatedAt     = p.CreatedAt
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response = PaginationResponse<GetSoftwarePackagesResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetSoftwarePackagesResponse>>.Success(response);
    }
}
