using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SoftwarePackages.UpdatePackageStatus;

/// <summary>
/// Handles <see cref="UpdatePackageStatusCommand"/>.
/// Modifies only the availability status of a software package.
/// </summary>
public sealed class UpdatePackageStatusHandler : ICommandHandler<UpdatePackageStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdatePackageStatusHandler"/>.</summary>
    public UpdatePackageStatusHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        UpdatePackageStatusCommand command,
        CancellationToken cancellationToken)
    {
        var package = await _unitOfWork.SoftwarePackages.GetByIdAsync(command.Id, cancellationToken);
        if (package is null)
            return Result.Failure(Error.NotFound("SoftwarePackage"));

        package.SetStatus(command.Status);

        _unitOfWork.SoftwarePackages.Update(package);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success($"Package status changed to {command.Status}.");
    }
}
