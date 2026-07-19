using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.SoftwarePackages.CreateSoftwarePackage;

/// <summary>
/// Handles <see cref="CreateSoftwarePackageCommand"/>.
/// Creates and persists a new <see cref="SoftwarePackage"/> entity.
/// </summary>
public sealed class CreateSoftwarePackageHandler
    : ICommandHandler<CreateSoftwarePackageCommand, CreateSoftwarePackageResponse>
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
            command.Category,
            command.Version,
            command.Description,
            command.SetupFileUrl,
            command.IconUrl);

        await _unitOfWork.SoftwarePackages.AddAsync(package, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateSoftwarePackageResponse>.Success(
            new CreateSoftwarePackageResponse
            {
                Id        = package.Id,
                Name      = package.Name,
                Category  = package.Category,
                Version   = package.Version,
                CreatedAt = package.CreatedAt
            },
            "Software package uploaded successfully.");
    }
}
