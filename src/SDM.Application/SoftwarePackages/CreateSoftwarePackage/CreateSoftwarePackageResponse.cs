using SDM.Domain.Enums;

namespace SDM.Application.SoftwarePackages.CreateSoftwarePackage;

/// <summary>
/// Confirmation response returning the newly created package info.
/// </summary>
public sealed class CreateSoftwarePackageResponse
{
    /// <summary>Gets the unique identifier of the new package.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the package name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the package version.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Gets the installer type.</summary>
    public InstallerType InstallerType { get; init; }

    /// <summary>Gets the current status.</summary>
    public PackageStatus Status { get; init; }

    /// <summary>Gets the UTC creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
}
