using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SystemComponents.GetSystemComponent;

/// <summary>
/// Handles <see cref="GetSystemComponentQuery"/> by querying <see cref="IReadDbContext"/>.
/// </summary>
public sealed class GetSystemComponentHandler : IQueryHandler<GetSystemComponentQuery, GetSystemComponentResponse>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetSystemComponentHandler"/>.</summary>
    public GetSystemComponentHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<GetSystemComponentResponse>> Handle(
        GetSystemComponentQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _db.SystemComponents
            .Where(c => c.Id == query.Id)
            .Select(c => new GetSystemComponentResponse
            {
                Id                   = c.Id,
                Name                 = c.Name,
                Version              = c.Version,
                FilePath             = c.FilePath,
                SilentInstallCommand = c.SilentInstallCommand,
                SHA256               = c.SHA256,
                Size                 = c.Size,
                RequiresRestart      = c.RequiresRestart,
                Status               = c.Status,
                CreatedAt            = c.CreatedAt,
                CreatedBy            = c.CreatedBy,
                UpdatedAt            = c.UpdatedAt,
                UpdatedBy            = c.UpdatedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<GetSystemComponentResponse>.Failure(Error.NotFound("SystemComponent"));

        return Result<GetSystemComponentResponse>.Success(response);
    }
}
