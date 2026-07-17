using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

/// <summary>
/// Handles <see cref="UpdateSoftwarePackageCommand"/>.
/// Updates an existing software package's editable metadata.
/// </summary>
public sealed class UpdateSoftwarePackageHandler : ICommandHandler<UpdateSoftwarePackageCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateSoftwarePackageHandler"/>.</summary>
    public UpdateSoftwarePackageHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        UpdateSoftwarePackageCommand command,
        CancellationToken cancellationToken)
    {
        var package = await _unitOfWork.SoftwarePackages.GetByIdAsync(command.Id, cancellationToken);
        if (package is null)
            return Result.Failure(Error.NotFound("SoftwarePackage"));

        package.Update(
            command.Name,
            command.Version,
            command.Category,
            command.Description,
            command.FilePath,
            command.SilentInstallCommand,
            command.DetectionRule,
            command.SHA256.ToLowerInvariant(),
            command.Size,
            command.InstallerType,
            command.RequiresRestart);

        _unitOfWork.SoftwarePackages.Update(package);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Software package updated successfully.");
    }
}
