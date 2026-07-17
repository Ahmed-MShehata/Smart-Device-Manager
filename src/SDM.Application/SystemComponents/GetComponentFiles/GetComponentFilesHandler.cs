using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SystemComponents.GetComponentFiles;

/// <summary>
/// Handles <see cref="GetComponentFilesQuery"/>.
/// Returns safe file metadata — never exposes physical paths.
/// </summary>
public sealed class GetComponentFilesHandler : IQueryHandler<GetComponentFilesQuery, List<ComponentFileRecord>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetComponentFilesHandler"/>.</summary>
    public GetComponentFilesHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<List<ComponentFileRecord>>> Handle(GetComponentFilesQuery query, CancellationToken cancellationToken)
    {
        var componentExists = await _db.SystemComponents
            .AnyAsync(c => c.Id == query.ComponentId, cancellationToken);

        if (!componentExists)
            return Result<List<ComponentFileRecord>>.Failure(Error.NotFound("SystemComponent"));

        var files = await _db.ComponentFiles
            .Where(f => f.ComponentId == query.ComponentId)
            .OrderBy(f => f.UploadedAt)
            .Select(f => new ComponentFileRecord
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

        return Result<List<ComponentFileRecord>>.Success(files, "Component files retrieved successfully.");
    }
}
