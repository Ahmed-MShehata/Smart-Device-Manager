using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents metadata for a file attached to a <see cref="SoftwarePackage"/>.
/// Physical file storage is handled by Infrastructure — this entity holds only safe references.
/// Never store server-side absolute paths here; use relative paths only.
/// </summary>
public class PackageFile : BaseEntity
{
    /// <summary>Gets the parent software package identifier.</summary>
    public Guid PackageId { get; private set; }

    /// <summary>Gets the purpose / type classification of this file.</summary>
    public FileReferenceType FileType { get; private set; }

    /// <summary>Gets the unique generated file name stored on disk (never the client-supplied name).</summary>
    public string StoredFileName { get; private set; } = string.Empty;

    /// <summary>Gets the original file name supplied by the uploader (display only).</summary>
    public string OriginalFileName { get; private set; } = string.Empty;

    /// <summary>Gets the relative storage path (e.g., <c>files/packages/abc123.exe</c>). Never an absolute path.</summary>
    public string RelativePath { get; private set; } = string.Empty;

    /// <summary>Gets the file size in bytes.</summary>
    public long FileSize { get; private set; }

    /// <summary>Gets the MIME type (e.g., <c>application/x-msdownload</c>).</summary>
    public string MimeType { get; private set; } = string.Empty;

    /// <summary>Gets the SHA-256 hex hash for integrity verification.</summary>
    public string SHA256 { get; private set; } = string.Empty;

    /// <summary>Gets the optional version string for this specific file (e.g., "14.36.0").</summary>
    public string? Version { get; private set; }

    /// <summary>Gets the UTC timestamp when this file was registered.</summary>
    public DateTime UploadedAt { get; private set; }

    // Navigation
    /// <summary>Gets the parent software package.</summary>
    public SoftwarePackage Package { get; private set; } = null!;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected PackageFile() { }

    /// <summary>
    /// Creates a new <see cref="PackageFile"/> record.
    /// </summary>
    public PackageFile(
        Guid packageId,
        FileReferenceType fileType,
        string storedFileName,
        string originalFileName,
        string relativePath,
        long fileSize,
        string mimeType,
        string sha256,
        string? version)
    {
        PackageId        = packageId;
        FileType         = fileType;
        StoredFileName   = storedFileName;
        OriginalFileName = originalFileName;
        RelativePath     = relativePath;
        FileSize         = fileSize;
        MimeType         = mimeType;
        SHA256           = sha256;
        Version          = version;
        UploadedAt       = DateTime.UtcNow;
    }
}
