using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a required Windows runtime component (e.g., .NET Runtime, Visual C++, DirectX)
/// that must be installed as a prerequisite before software packages can run.
/// Audit fields (<c>CreatedBy</c>, <c>UpdatedBy</c>, <c>UpdatedAt</c>) are stamped
/// automatically by Infrastructure — never by this entity.
/// </summary>
public class SystemComponent : AuditableEntity
{
    /// <summary>Gets the display name of the component.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Gets the version string (e.g., "14.36.32532").</summary>
    public string Version { get; private set; } = string.Empty;

    /// <summary>Gets the server-side file path or download URL.</summary>
    public string FilePath { get; private set; } = string.Empty;

    /// <summary>Gets the command used to silently install this component.</summary>
    public string SilentInstallCommand { get; private set; } = string.Empty;

    /// <summary>Gets the SHA-256 hash of the installer file. Must be exactly 64 lowercase hex characters.</summary>
    public string SHA256 { get; private set; } = string.Empty;

    /// <summary>Gets the file size in bytes. Must be greater than zero.</summary>
    public long Size { get; private set; }

    /// <summary>Gets a value indicating whether installing this component requires a system restart.</summary>
    public bool RequiresRestart { get; private set; }

    /// <summary>Gets the current availability status of this component.</summary>
    public ComponentStatus Status { get; private set; } = ComponentStatus.Active;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected SystemComponent() { }

    /// <summary>
    /// Creates a new <see cref="SystemComponent"/>.
    /// Status defaults to <see cref="ComponentStatus.Active"/>.
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    /// <param name="name">Display name. Required.</param>
    /// <param name="version">Version string. Required.</param>
    /// <param name="filePath">Server file path or URL. Required.</param>
    /// <param name="silentInstallCommand">Silent install command. Required and non-empty.</param>
    /// <param name="sha256">64-character lowercase hex SHA-256 hash. Required.</param>
    /// <param name="size">File size in bytes. Must be greater than zero.</param>
    /// <param name="requiresRestart">Whether a restart is required after installation.</param>
    public SystemComponent(
        string name,
        string version,
        string filePath,
        string silentInstallCommand,
        string sha256,
        long size,
        bool requiresRestart = false)
    {
        Name = name;
        Version = version;
        FilePath = filePath;
        SilentInstallCommand = silentInstallCommand;
        SHA256 = sha256;
        Size = size;
        RequiresRestart = requiresRestart;
        Status = ComponentStatus.Active;
    }

    /// <summary>Sets the availability status of this component.</summary>
    /// <param name="status">The new <see cref="ComponentStatus"/>.</param>
    public void SetStatus(ComponentStatus status)
    {
        Status = status;
    }

    /// <summary>Updates the editable metadata of this component.</summary>
    public void Update(
        string name,
        string version,
        string filePath,
        string silentInstallCommand,
        string sha256,
        long size,
        bool requiresRestart)
    {
        Name = name;
        Version = version;
        FilePath = filePath;
        SilentInstallCommand = silentInstallCommand;
        SHA256 = sha256;
        Size = size;
        RequiresRestart = requiresRestart;
    }
}
