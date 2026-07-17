using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Settings.GetSettings;

/// <summary>
/// Handles <see cref="GetSettingsQuery"/> returning data via <see cref="IReadDbContext"/>.
/// </summary>
public sealed class GetSettingsHandler : IQueryHandler<GetSettingsQuery, PaginationResponse<GetSettingsResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetSettingsHandler"/>.</summary>
    public GetSettingsHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<PaginationResponse<GetSettingsResponse>>> Handle(
        GetSettingsQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.Settings.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(s =>
                EF.Functions.Like(s.Key, $"%{query.Search}%") ||
                (s.Description != null && EF.Functions.Like(s.Description, $"%{query.Search}%")));
        }

        if (query.IsPublic.HasValue)
            q = q.Where(s => s.IsPublic == query.IsPublic.Value);

        // Settings are always sorted alphabetically by Key by default to make them easy to browse
        q = q.OrderBy(s => s.Key);

        var totalCount = await q.CountAsync(cancellationToken);

        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(s => new GetSettingsResponse
            {
                Id          = s.Id,
                Key         = s.Key,
                Value       = s.Value,
                Description = s.Description,
                IsPublic    = s.IsPublic
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response = PaginationResponse<GetSettingsResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetSettingsResponse>>.Success(response);
    }
}
