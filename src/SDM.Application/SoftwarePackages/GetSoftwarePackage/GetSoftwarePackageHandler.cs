using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackage;

/// <summary>Handles <see cref="GetSoftwarePackageQuery"/> by querying <see cref="IReadDbContext"/>.</summary>
public sealed class GetSoftwarePackageHandler : IQueryHandler<GetSoftwarePackageQuery, GetSoftwarePackageResponse>
{
    private readonly IReadDbContext _db;

    public GetSoftwarePackageHandler(IReadDbContext db) => _db = db;

    public async Task<Result<GetSoftwarePackageResponse>> Handle(
        GetSoftwarePackageQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _db.SoftwarePackages
            .Where(p => p.Id == query.Id)
            .Select(p => new GetSoftwarePackageResponse
            {
                Id           = p.Id,
                Name         = p.Name,
                Category     = p.Category,
                Version      = p.Version,
                Description  = p.Description,
                IconUrl      = p.IconUrl,
                SetupFileUrl = p.SetupFileUrl,
                CreatedAt    = p.CreatedAt,
                CreatedBy    = p.CreatedBy,
                UpdatedAt    = p.UpdatedAt,
                UpdatedBy    = p.UpdatedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result<GetSoftwarePackageResponse>.Failure(Error.NotFound("SoftwarePackage"));

        return Result<GetSoftwarePackageResponse>.Success(response);
    }
}
