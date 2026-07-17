using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.SoftwarePackages.CreateSoftwarePackage;

/// <summary>
/// Handles <see cref="CreateSoftwarePackageCommand"/>.
/// Maps the input to a new <see cref="SoftwarePackage"/> entity and commits to the database via UnitOfWork.
/// </summary>
public sealed class CreateSoftwarePackageHandler : ICommandHandler<CreateSoftwarePackageCommand, CreateSoftwarePackageResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="CreateSoftwarePackageHandler"/>.</summary>
    public CreateSoftwarePackageHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<CreateSoftwarePackageResponse>> Handle(
        CreateSoftwarePackageCommand command,
        CancellationToken cancellationToken)
    {
        var package = new SoftwarePackage(
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

        await _unitOfWork.SoftwarePackages.AddAsync(package, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateSoftwarePackageResponse>.Success(
            new CreateSoftwarePackageResponse
            {
                Id            = package.Id,
                Name          = package.Name,
                Version       = package.Version,
                InstallerType = package.InstallerType,
                Status        = package.Status,
                CreatedAt     = package.CreatedAt
            },
            "Software package registered successfully.");
    }
}
