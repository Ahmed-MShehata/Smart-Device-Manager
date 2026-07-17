using SDM.Domain.Enums;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>
/// Summary DTO for a single software package in a paginated list.
/// </summary>
public sealed class GetSoftwarePackagesResponse
{
    /// <summary>Unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Display name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Version string.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Category string.</summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>Size in bytes.</summary>
    public long Size { get; init; }

    /// <summary>Installer format type.</summary>
    public InstallerType InstallerType { get; init; }

    /// <summary>Availability status.</summary>
    public PackageStatus Status { get; init; }

    /// <summary>UTC creation time.</summary>
    public DateTime CreatedAt { get; init; }
}
