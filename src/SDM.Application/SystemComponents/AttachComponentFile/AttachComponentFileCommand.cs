using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.AttachComponentFile;

/// <summary>
/// Command to register a new file reference against an existing system component.
/// </summary>
public sealed class AttachComponentFileCommand : ICommand<AttachComponentFileResponse>
{
    /// <summary>Gets the parent component identifier. Supplied by the route.</summary>
    public Guid ComponentId { get; init; }

    /// <summary>Gets the file type classification.</summary>
    public FileReferenceType FileType { get; init; }

    /// <summary>Gets the unique stored file name (no path component).</summary>
    public string StoredFileName { get; init; } = string.Empty;

    /// <summary>Gets the original client-side file name (display only).</summary>
    public string OriginalFileName { get; init; } = string.Empty;

    /// <summary>Gets the relative storage path. Must not be an absolute server path.</summary>
    public string RelativePath { get; init; } = string.Empty;

    /// <summary>Gets the file size in bytes.</summary>
    public long FileSize { get; init; }

    /// <summary>Gets the MIME type of the file.</summary>
    public string MimeType { get; init; } = string.Empty;

    /// <summary>Gets the SHA-256 hex hash for integrity verification.</summary>
    public string SHA256 { get; init; } = string.Empty;

    /// <summary>Gets the optional version string for this file.</summary>
    public string? Version { get; init; }
}
