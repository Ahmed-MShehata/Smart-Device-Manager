using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.SoftwarePackages.UpdatePackageStatus;

/// <summary>
/// Command to change the availability status of a software package.
/// </summary>
public sealed class UpdatePackageStatusCommand : ICommand
{
    /// <summary>Gets the unique identifier of the package. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the new status to apply.</summary>
    public PackageStatus Status { get; init; }
}
