using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Settings.GetSetting;

/// <summary>
/// Handles <see cref="GetSettingQuery"/> utilizing <see cref="IReadDbContext"/>.
/// </summary>
public sealed class GetSettingHandler : IQueryHandler<GetSettingQuery, GetSettingResponse>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetSettingHandler"/>.</summary>
    public GetSettingHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<GetSettingResponse>> Handle(
        GetSettingQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _db.Settings
            .Where(s => s.Id == query.Id)
            .Select(s => new GetSettingResponse
            {
                Id          = s.Id,
                Key         = s.Key,
                Value       = s.Value,
                Description = s.Description,
                IsPublic    = s.IsPublic,
                CreatedAt   = s.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<GetSettingResponse>.Failure(Error.NotFound("Setting"));

        return Result<GetSettingResponse>.Success(response);
    }
}
