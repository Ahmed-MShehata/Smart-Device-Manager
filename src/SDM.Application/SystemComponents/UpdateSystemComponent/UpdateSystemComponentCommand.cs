using SDM.Application.Common.CQRS;

namespace SDM.Application.SystemComponents.UpdateSystemComponent;

/// <summary>
/// Command to update the details of an existing system component.
/// </summary>
public sealed class UpdateSystemComponentCommand : ICommand
{
    /// <summary>Gets the unique identifier of the component. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the display name of the component. Required.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the version string. Required.</summary>
    public string Version { get; init; } = string.Empty;

    /// <summary>Gets the server-side file path or download URL. Required.</summary>
    public string FilePath { get; init; } = string.Empty;

    /// <summary>Gets the command used to silently install this component. Required.</summary>
    public string SilentInstallCommand { get; init; } = string.Empty;

    /// <summary>Gets the 64-character lowercase hex SHA-256 hash. Required.</summary>
    public string SHA256 { get; init; } = string.Empty;

    /// <summary>Gets the file size in bytes. Must be greater than zero.</summary>
    public long Size { get; init; }

    /// <summary>Gets whether this component requires a restart after installation.</summary>
    public bool RequiresRestart { get; init; }
}
