using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.SoftwarePackages.AttachPackageFile;

/// <summary>
/// Handles <see cref="AttachPackageFileCommand"/>.
/// </summary>
public sealed class AttachPackageFileHandler : ICommandHandler<AttachPackageFileCommand, AttachPackageFileResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="AttachPackageFileHandler"/>.</summary>
    public AttachPackageFileHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<AttachPackageFileResponse>> Handle(AttachPackageFileCommand command, CancellationToken cancellationToken)
    {
        var packageExists = await _unitOfWork.SoftwarePackages.ExistsAsync(command.PackageId, cancellationToken);
        if (!packageExists)
            return Result<AttachPackageFileResponse>.Failure(Error.NotFound("SoftwarePackage"));

        var file = new PackageFile(
            command.PackageId,
            command.FileType,
            command.StoredFileName,
            command.OriginalFileName,
            command.RelativePath,
            command.FileSize,
            command.MimeType,
            command.SHA256,
            command.Version);

        await _unitOfWork.PackageFiles.AddAsync(file, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AttachPackageFileResponse>.Success(
            new AttachPackageFileResponse { FileId = file.Id },
            "File attached to software package successfully.");
    }
}
