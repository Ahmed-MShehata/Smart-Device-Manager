using SDM.Domain.Enums;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackage;

/// <summary>
/// Complete detail DTO for a software package.
/// </summary>
public sealed class GetSoftwarePackageResponse
{
    /// <summary>Gets the unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the display name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the version string.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Gets the category.</summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>Gets the description.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Gets the server-side file path or download URL.</summary>
    public string FilePath { get; init; } = string.Empty;

    /// <summary>Gets the command used to silently install this package.</summary>
    public string SilentInstallCommand { get; init; } = string.Empty;

    /// <summary>Gets the logic used to detect whether this package is installed.</summary>
    public string DetectionRule { get; init; } = string.Empty;

    /// <summary>Gets the SHA-256 hash.</summary>
    public string SHA256 { get; init; } = string.Empty;

    /// <summary>Gets the file size in bytes.</summary>
    public long Size { get; init; }

    /// <summary>Gets the installer format type.</summary>
    public InstallerType InstallerType { get; init; }

    /// <summary>Gets whether this package requires a restart.</summary>
    public bool RequiresRestart { get; init; }

    /// <summary>Gets the package status.</summary>
    public PackageStatus Status { get; init; }

    /// <summary>Gets the creation UTC time.</summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>Gets the user who created it.</summary>
    public string CreatedBy { get; init; } = string.Empty;

    /// <summary>Gets the update UTC time. Null if never updated.</summary>
    public DateTime? UpdatedAt { get; init; }

    /// <summary>Gets the user who last updated it. Null if never updated.</summary>
    public string? UpdatedBy { get; init; }
}
