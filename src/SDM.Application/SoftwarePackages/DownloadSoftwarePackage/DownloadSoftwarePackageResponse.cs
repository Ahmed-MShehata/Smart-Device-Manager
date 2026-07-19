namespace SDM.Application.SoftwarePackages.DownloadSoftwarePackage;

/// <summary>
/// Response carrying the information needed to serve the file download.
/// The controller uses this to return a file stream — the internal path
/// is never sent to the client in the response body.
/// </summary>
public sealed class DownloadSoftwarePackageResponse
{
    /// <summary>Relative path stored in the database (used by Infrastructure to resolve the stream).</summary>
    public string RelativePath { get; init; } = string.Empty;

    /// <summary>Original file name to present to the browser/client.</summary>
    public string FileName { get; init; } = string.Empty;

    /// <summary>MIME content-type for the file.</summary>
    public string ContentType { get; init; } = "application/octet-stream";
}
