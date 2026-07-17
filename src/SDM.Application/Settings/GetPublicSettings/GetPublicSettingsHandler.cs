using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Settings.GetPublicSettings;

/// <summary>
/// Handles <see cref="GetPublicSettingsQuery"/> via <see cref="IReadDbContext"/>.
/// Returns only settings marked as <c>IsPublic</c>.
/// </summary>
public sealed class GetPublicSettingsHandler : IQueryHandler<GetPublicSettingsQuery, IReadOnlyList<GetPublicSettingsResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetPublicSettingsHandler"/>.</summary>
    public GetPublicSettingsHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<IReadOnlyList<GetPublicSettingsResponse>>> Handle(
        GetPublicSettingsQuery query,
        CancellationToken cancellationToken)
    {
        var settings = await _db.Settings
            .Where(s => s.IsPublic)
            .OrderBy(s => s.Key)
            .Select(s => new GetPublicSettingsResponse
            {
                Key   = s.Key,
                Value = s.Value
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<GetPublicSettingsResponse>>.Success(settings);
    }
}
