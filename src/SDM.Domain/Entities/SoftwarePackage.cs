using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a software package that the admin uploads and customers can silently install.
/// Audit fields (<c>CreatedBy</c>, <c>UpdatedBy</c>, <c>UpdatedAt</c>) are stamped
/// automatically by Infrastructure — never by this entity.
/// </summary>
public class SoftwarePackage : AuditableEntity
{
    /// <summary>Gets the display name of the package.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Gets the version string (e.g., "1.0.0").</summary>
    public string Version { get; private set; } = string.Empty;

    /// <summary>Gets the category of this package (e.g., Browser, Antivirus).</summary>
    public string Category { get; private set; } = string.Empty;

    /// <summary>Gets the description explaining what this package does.</summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>Gets the server-side file path or download URL.</summary>
    public string FilePath { get; private set; } = string.Empty;

    /// <summary>Gets the command used to silently install this package (e.g., "/S /quiet").</summary>
    public string SilentInstallCommand { get; private set; } = string.Empty;

    /// <summary>Gets the logic used to detect whether this package is already installed.</summary>
    public string DetectionRule { get; private set; } = string.Empty;

    /// <summary>Gets the SHA-256 hash of the installer file. Must be exactly 64 lowercase hex characters.</summary>
    public string SHA256 { get; private set; } = string.Empty;

    /// <summary>Gets the file size in bytes. Must be greater than zero.</summary>
    public long Size { get; private set; }

    /// <summary>Gets the installer format type (EXE, MSI, or ZIP).</summary>
    public InstallerType InstallerType { get; private set; }

    /// <summary>Gets a value indicating whether installing this package requires a system restart.</summary>
    public bool RequiresRestart { get; private set; }

    /// <summary>Gets the current availability status of this package.</summary>
    public PackageStatus Status { get; private set; } = PackageStatus.Active;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected SoftwarePackage() { }

    /// <summary>
    /// Creates a new <see cref="SoftwarePackage"/>.
    /// Status defaults to <see cref="PackageStatus.Active"/>.
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    /// <param name="name">Display name. Required.</param>
    /// <param name="version">Version string. Required.</param>
    /// <param name="category">Package category. Required.</param>
    /// <param name="description">Package description. Required.</param>
    /// <param name="filePath">Server file path or URL. Required.</param>
    /// <param name="silentInstallCommand">Silent install command. Required and non-empty.</param>
    /// <param name="detectionRule">Detection logic string. Required and non-empty.</param>
    /// <param name="sha256">64-character lowercase hex SHA-256 hash. Required.</param>
    /// <param name="size">File size in bytes. Must be greater than zero.</param>
    /// <param name="installerType">Installer format (EXE, MSI, ZIP).</param>
    /// <param name="requiresRestart">Whether a restart is required after installation.</param>
    public SoftwarePackage(
        string name,
        string version,
        string category,
        string description,
        string filePath,
        string silentInstallCommand,
        string detectionRule,
        string sha256,
        long size,
        InstallerType installerType,
        bool requiresRestart = false)
    {
        Name = name;
        Version = version;
        Category = category;
        Description = description;
        FilePath = filePath;
        SilentInstallCommand = silentInstallCommand;
        DetectionRule = detectionRule;
        SHA256 = sha256;
        Size = size;
        InstallerType = installerType;
        RequiresRestart = requiresRestart;
        Status = PackageStatus.Active;
    }

    /// <summary>Sets the availability status of this package.</summary>
    /// <param name="status">The new <see cref="PackageStatus"/>.</param>
    public void SetStatus(PackageStatus status)
    {
        Status = status;
    }

    /// <summary>Updates the editable metadata of this package.</summary>
    public void Update(
        string name,
        string version,
        string category,
        string description,
        string filePath,
        string silentInstallCommand,
        string detectionRule,
        string sha256,
        long size,
        InstallerType installerType,
        bool requiresRestart)
    {
        Name = name;
        Version = version;
        Category = category;
        Description = description;
        FilePath = filePath;
        SilentInstallCommand = silentInstallCommand;
        DetectionRule = detectionRule;
        SHA256 = sha256;
        Size = size;
        InstallerType = installerType;
        RequiresRestart = requiresRestart;
    }
}
