using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackage;

/// <summary>
/// Handles <see cref="GetSoftwarePackageQuery"/> by querying <see cref="IReadDbContext"/>.
/// </summary>
public sealed class GetSoftwarePackageHandler : IQueryHandler<GetSoftwarePackageQuery, GetSoftwarePackageResponse>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetSoftwarePackageHandler"/>.</summary>
    public GetSoftwarePackageHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<GetSoftwarePackageResponse>> Handle(
        GetSoftwarePackageQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _db.SoftwarePackages
            .Where(p => p.Id == query.Id)
            .Select(p => new GetSoftwarePackageResponse
            {
                Id                   = p.Id,
                Name                 = p.Name,
                Version              = p.Version,
                Category             = p.Category,
                Description          = p.Description,
                FilePath             = p.FilePath,
                SilentInstallCommand = p.SilentInstallCommand,
                DetectionRule        = p.DetectionRule,
                SHA256               = p.SHA256,
                Size                 = p.Size,
                InstallerType        = p.InstallerType,
                RequiresRestart      = p.RequiresRestart,
                Status               = p.Status,
                CreatedAt            = p.CreatedAt,
                CreatedBy            = p.CreatedBy,
                UpdatedAt            = p.UpdatedAt,
                UpdatedBy            = p.UpdatedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<GetSoftwarePackageResponse>.Failure(Error.NotFound("SoftwarePackage"));

        return Result<GetSoftwarePackageResponse>.Success(response);
    }
}
