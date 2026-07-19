using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

/// <summary>
/// Command to update an existing software package.
/// Split into two operation modes:
///   1. Metadata update only (Name, Category, Description, IconUrl — no file replacement).
///   2. Setup file replacement — new Version and SetupFileUrl replace the existing ones.
///      Metadata fields are still updated in the same call.
/// The handler detects whether a new setup file was provided via <see cref="NewSetupFileUrl"/>.
/// </summary>
public sealed class UpdateSoftwarePackageCommand : ICommand
{
    /// <summary>Gets the unique identifier of the package. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the updated display name. Required.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the updated category (Application or Driver). Required.</summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>Gets the updated description. Required.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Gets the updated icon file path. Null to keep the existing icon.</summary>
    public string? IconUrl { get; init; }

    /// <summary>
    /// Gets the new setup file path when the admin is replacing the installer.
    /// Null means metadata-only update (Version stays the same).
    /// </summary>
    public string? NewSetupFileUrl { get; init; }

    /// <summary>
    /// Gets the new version string when replacing the setup file.
    /// Required when <see cref="NewSetupFileUrl"/> is provided.
    /// </summary>
    public string? NewVersion { get; init; }
}
