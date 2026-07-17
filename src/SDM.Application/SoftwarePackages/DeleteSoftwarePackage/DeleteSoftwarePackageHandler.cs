using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Enums;

namespace SDM.Application.SoftwarePackages.DeleteSoftwarePackage;

/// <summary>
/// Handles <see cref="DeleteSoftwarePackageCommand"/>.
/// Performs a soft delete by marking the <see cref="Domain.Entities.SoftwarePackage"/> as <c>Deprecated</c>.
/// </summary>
public sealed class DeleteSoftwarePackageHandler : ICommandHandler<DeleteSoftwarePackageCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="DeleteSoftwarePackageHandler"/>.</summary>
    public DeleteSoftwarePackageHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        DeleteSoftwarePackageCommand command,
        CancellationToken cancellationToken)
    {
        var package = await _unitOfWork.SoftwarePackages.GetByIdAsync(command.Id, cancellationToken);
        if (package is null)
            return Result.Failure(Error.NotFound("SoftwarePackage"));

        // Soft delete
        package.SetStatus(PackageStatus.Deprecated);

        _unitOfWork.SoftwarePackages.Update(package);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Software package deleted (deprecated) successfully.");
    }
}
