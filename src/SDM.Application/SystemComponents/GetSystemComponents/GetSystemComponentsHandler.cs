using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SystemComponents.GetSystemComponents;

/// <summary>
/// Handles <see cref="GetSystemComponentsQuery"/> returning data via <see cref="IReadDbContext"/>.
/// </summary>
public sealed class GetSystemComponentsHandler : IQueryHandler<GetSystemComponentsQuery, PaginationResponse<GetSystemComponentsResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetSystemComponentsHandler"/>.</summary>
    public GetSystemComponentsHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<PaginationResponse<GetSystemComponentsResponse>>> Handle(
        GetSystemComponentsQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.SystemComponents.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(c =>
                EF.Functions.Like(c.Name, $"%{query.Search}%") ||
                EF.Functions.Like(c.Version, $"%{query.Search}%"));
        }

        if (query.Status.HasValue)
            q = q.Where(c => c.Status == query.Status.Value);

        q = (query.SortBy, query.Descending) switch
        {
            (SystemComponentSortBy.Name, false)      => q.OrderBy(c => c.Name),
            (SystemComponentSortBy.Name, true)       => q.OrderByDescending(c => c.Name),
            (SystemComponentSortBy.Size, false)      => q.OrderBy(c => c.Size),
            (SystemComponentSortBy.Size, true)       => q.OrderByDescending(c => c.Size),
            (SystemComponentSortBy.CreatedAt, false) => q.OrderBy(c => c.CreatedAt),
            (SystemComponentSortBy.CreatedAt, true)  => q.OrderByDescending(c => c.CreatedAt),
            _                                        => q.OrderBy(c => c.Name)
        };

        var totalCount = await q.CountAsync(cancellationToken);

        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new GetSystemComponentsResponse
            {
                Id        = c.Id,
                Name      = c.Name,
                Version   = c.Version,
                Size      = c.Size,
                Status    = c.Status,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response = PaginationResponse<GetSystemComponentsResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetSystemComponentsResponse>>.Success(response);
    }
}
