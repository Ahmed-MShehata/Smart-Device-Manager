using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.GetComponentFiles;

/// <summary>
/// Safe download metadata DTO for a file attached to a system component.
/// Physical paths are never included.
/// </summary>
public sealed class ComponentFileRecord
{
    /// <summary>Gets the file record identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the file type classification.</summary>
    public FileReferenceType FileType { get; init; }

    /// <summary>Gets the original client-supplied file name (display only).</summary>
    public string OriginalFileName { get; init; } = string.Empty;

    /// <summary>Gets the file size in bytes.</summary>
    public long FileSize { get; init; }

    /// <summary>Gets the MIME type.</summary>
    public string MimeType { get; init; } = string.Empty;

    /// <summary>Gets the SHA-256 hash for integrity verification.</summary>
    public string SHA256 { get; init; } = string.Empty;

    /// <summary>Gets the optional version string.</summary>
    public string? Version { get; init; }

    /// <summary>Gets the UTC timestamp when this file was registered.</summary>
    public DateTime UploadedAt { get; init; }
}
