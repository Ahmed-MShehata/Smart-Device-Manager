using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.GetSystemComponent;

/// <summary>
/// Complete detail DTO for a system component.
/// </summary>
public sealed class GetSystemComponentResponse
{
    /// <summary>Gets the unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the display name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the version string.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Gets the server-side file path or download URL.</summary>
    public string FilePath { get; init; } = string.Empty;

    /// <summary>Gets the command used to silently install this component.</summary>
    public string SilentInstallCommand { get; init; } = string.Empty;

    /// <summary>Gets the SHA-256 hash.</summary>
    public string SHA256 { get; init; } = string.Empty;

    /// <summary>Gets the file size in bytes.</summary>
    public long Size { get; init; }

    /// <summary>Gets whether this component requires a restart.</summary>
    public bool RequiresRestart { get; init; }

    /// <summary>Gets the component status.</summary>
    public ComponentStatus Status { get; init; }

    /// <summary>Gets the creation UTC time.</summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>Gets the user who created it.</summary>
    public string CreatedBy { get; init; } = string.Empty;

    /// <summary>Gets the update UTC time. Null if never updated.</summary>
    public DateTime? UpdatedAt { get; init; }

    /// <summary>Gets the user who last updated it. Null if never updated.</summary>
    public string? UpdatedBy { get; init; }
}
