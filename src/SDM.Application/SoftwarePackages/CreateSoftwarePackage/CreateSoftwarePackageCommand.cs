using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.CreateSoftwarePackage;

/// <summary>
/// Command to upload a new software package (application or driver).
/// The setup file is uploaded via multipart form — the resolved server path
/// is passed here as <see cref="SetupFileUrl"/>.
/// Handled by <see cref="CreateSoftwarePackageHandler"/>.
/// </summary>
public sealed class CreateSoftwarePackageCommand : ICommand<CreateSoftwarePackageResponse>
{
    /// <summary>Gets the display name of the software. Required.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the category: Application or Driver. Required.</summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>Gets the version string (e.g., "1.0.0"). Required.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Gets the admin-authored description. Required.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Gets the stored setup file path after upload. Required.</summary>
    public string SetupFileUrl { get; init; } = string.Empty;

    /// <summary>Gets the stored icon file path after upload. Optional.</summary>
    public string? IconUrl { get; init; }
}
