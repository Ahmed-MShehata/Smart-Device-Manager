using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.DeleteSoftwarePackage;

/// <summary>Command to permanently delete a software package and its associated setup file.</summary>
public sealed class DeleteSoftwarePackageCommand : ICommand
{
    public Guid Id { get; init; }
}
