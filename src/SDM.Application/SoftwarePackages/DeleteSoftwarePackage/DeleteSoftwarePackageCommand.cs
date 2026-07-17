using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.DeleteSoftwarePackage;

/// <summary>
/// Command to soft-delete a software package.
/// This translates to setting its status to deprecated/inactive.
/// </summary>
public sealed class DeleteSoftwarePackageCommand : ICommand
{
    /// <summary>Gets the unique identifier of the package. Supplied by the route.</summary>
    public Guid Id { get; init; }
}
