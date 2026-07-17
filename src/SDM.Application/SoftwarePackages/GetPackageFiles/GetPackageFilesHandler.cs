using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.GetPackageFiles;

/// <summary>
/// Handles <see cref="GetPackageFilesQuery"/>.
/// Returns safe file metadata — never exposes physical paths.
/// </summary>
public sealed class GetPackageFilesHandler : IQueryHandler<GetPackageFilesQuery, List<PackageFileRecord>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetPackageFilesHandler"/>.</summary>
    public GetPackageFilesHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<List<PackageFileRecord>>> Handle(GetPackageFilesQuery query, CancellationToken cancellationToken)
    {
        var packageExists = await _db.SoftwarePackages
            .AnyAsync(p => p.Id == query.PackageId, cancellationToken);

        if (!packageExists)
            return Result<List<PackageFileRecord>>.Failure(Error.NotFound("SoftwarePackage"));

        var files = await _db.PackageFiles
            .Where(f => f.PackageId == query.PackageId)
            .OrderBy(f => f.UploadedAt)
            .Select(f => new PackageFileRecord
            {
                Id               = f.Id,
                FileType         = f.FileType,
                OriginalFileName = f.OriginalFileName,
                FileSize         = f.FileSize,
                MimeType         = f.MimeType,
                SHA256           = f.SHA256,
                Version          = f.Version,
                UploadedAt       = f.UploadedAt
            })
            .ToListAsync(cancellationToken);

        return Result<List<PackageFileRecord>>.Success(files, "Package files retrieved successfully.");
    }
}
