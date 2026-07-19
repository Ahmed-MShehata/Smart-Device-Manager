using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.DeleteSoftwarePackage;

/// <summary>
/// Handles <see cref="DeleteSoftwarePackageCommand"/>.
/// Removes the package record from the database and deletes the associated setup file from storage.
/// </summary>
public sealed class DeleteSoftwarePackageHandler : ICommandHandler<DeleteSoftwarePackageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorage;

    public DeleteSoftwarePackageHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorage)
    {
        _unitOfWork   = unitOfWork;
        _fileStorage  = fileStorage;
    }

    public async Task<Result> Handle(DeleteSoftwarePackageCommand command, CancellationToken cancellationToken)
    {
        var package = await _unitOfWork.SoftwarePackages.GetByIdAsync(command.Id, cancellationToken);

        if (package is null)
            return Result.Failure(Error.NotFound("SoftwarePackage"));

        // Remove setup file from disk (errors are swallowed by the storage service)
        if (!string.IsNullOrWhiteSpace(package.SetupFileUrl))
            await _fileStorage.DeleteAsync(package.SetupFileUrl, cancellationToken);

        _unitOfWork.SoftwarePackages.Remove(package);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Software package deleted successfully.");
    }
}
