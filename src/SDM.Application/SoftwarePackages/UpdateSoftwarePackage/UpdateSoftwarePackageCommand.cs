using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

/// <summary>
/// Command to update the details of an existing software package.
/// </summary>
public sealed class UpdateSoftwarePackageCommand : ICommand
{
    /// <summary>Gets the unique identifier of the package. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the display name of the package. Required.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the version string. Required.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Gets the category. Required.</summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>Gets the package description. Required.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Gets the server-side file path or download URL. Required.</summary>
    public string FilePath { get; init; } = string.Empty;

    /// <summary>Gets the command used to silently install this package. Required.</summary>
    public string SilentInstallCommand { get; init; } = string.Empty;

    /// <summary>Gets the logic used to detect whether this package is installed. Required.</summary>
    public string DetectionRule { get; init; } = string.Empty;

    /// <summary>Gets the 64-character lowercase hex SHA-256 hash. Required.</summary>
    public string SHA256 { get; init; } = string.Empty;

    /// <summary>Gets the file size in bytes. Must be greater than zero.</summary>
    public long Size { get; init; }

    /// <summary>Gets the installer format type. Required.</summary>
    public InstallerType InstallerType { get; init; }

    /// <summary>Gets whether this package requires a restart after installation.</summary>
    public bool RequiresRestart { get; init; }
}
