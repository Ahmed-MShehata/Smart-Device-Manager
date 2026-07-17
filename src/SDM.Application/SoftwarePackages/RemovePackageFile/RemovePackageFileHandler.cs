using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.RemovePackageFile;

/// <summary>
/// Handles <see cref="RemovePackageFileCommand"/>.
/// </summary>
public sealed class RemovePackageFileHandler : ICommandHandler<RemovePackageFileCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="RemovePackageFileHandler"/>.</summary>
    public RemovePackageFileHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(RemovePackageFileCommand command, CancellationToken cancellationToken)
    {
        var file = await _unitOfWork.PackageFiles.GetByIdAsync(command.Id, cancellationToken);
        if (file is null)
            return Result.Failure(Error.NotFound("PackageFile"));

        _unitOfWork.PackageFiles.Remove(file);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("File reference removed successfully.");
    }
}
