using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.DownloadSoftwarePackage;

/// <summary>
/// Handles <see cref="DownloadSoftwarePackageQuery"/>.
/// Resolves the internal setup file path without leaking it to the client.
/// The controller is responsible for serving the file stream.
/// </summary>
public sealed class DownloadSoftwarePackageHandler
    : IQueryHandler<DownloadSoftwarePackageQuery, DownloadSoftwarePackageResponse>
{
    private readonly IReadDbContext _db;

    public DownloadSoftwarePackageHandler(IReadDbContext db) => _db = db;

    public async Task<Result<DownloadSoftwarePackageResponse>> Handle(
        DownloadSoftwarePackageQuery query,
        CancellationToken cancellationToken)
    {
        var package = await _db.SoftwarePackages
            .Where(p => p.Id == query.Id)
            .Select(p => new { p.Name, p.SetupFileUrl })
            .FirstOrDefaultAsync(cancellationToken);

        if (package is null)
            return Result<DownloadSoftwarePackageResponse>.Failure(Error.NotFound("SoftwarePackage"));

        if (string.IsNullOrWhiteSpace(package.SetupFileUrl))
            return Result<DownloadSoftwarePackageResponse>.Failure(
                Error.Conflict("SoftwarePackage", "No setup file is associated with this package."));

        // Derive a safe download file name from the package name
        var safeFileName = string.Concat(
            package.Name.Split(Path.GetInvalidFileNameChars()))
            .Replace(' ', '_');

        var extension = Path.GetExtension(package.SetupFileUrl);
        safeFileName  = $"{safeFileName}{extension}";

        return Result<DownloadSoftwarePackageResponse>.Success(new DownloadSoftwarePackageResponse
        {
            RelativePath = package.SetupFileUrl,
            FileName     = safeFileName,
            ContentType  = "application/octet-stream"
        });
    }
}
