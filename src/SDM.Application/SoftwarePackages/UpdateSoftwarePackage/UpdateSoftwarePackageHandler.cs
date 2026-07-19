using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

/// <summary>
/// Handles <see cref="UpdateSoftwarePackageCommand"/>.
/// Applies metadata-only or metadata + file-replacement update depending on whether
/// <see cref="UpdateSoftwarePackageCommand.NewSetupFileUrl"/> is provided.
/// </summary>
public sealed class UpdateSoftwarePackageHandler : ICommandHandler<UpdateSoftwarePackageCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSoftwarePackageHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateSoftwarePackageCommand command,
        CancellationToken cancellationToken)
    {
        var package = await _unitOfWork.SoftwarePackages.GetByIdAsync(command.Id, cancellationToken);
        if (package is null)
            return Result.Failure(Error.NotFound("SoftwarePackage"));

        // Always update metadata
        package.UpdateMetadata(command.Name, command.Category, command.Description, command.IconUrl);

        // Replace setup file + version only when a new file was uploaded
        if (!string.IsNullOrWhiteSpace(command.NewSetupFileUrl) &&
            !string.IsNullOrWhiteSpace(command.NewVersion))
        {
            package.UpdateSetupFile(command.NewSetupFileUrl, command.NewVersion);
        }

        _unitOfWork.SoftwarePackages.Update(package);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Software package updated successfully.");
    }
}
