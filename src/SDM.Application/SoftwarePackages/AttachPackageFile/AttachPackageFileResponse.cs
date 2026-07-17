namespace SDM.Application.SoftwarePackages.AttachPackageFile;

/// <summary>
/// Confirmation response after attaching a file to a software package.
/// </summary>
public sealed class AttachPackageFileResponse
{
    /// <summary>Newly assigned file record identifier.</summary>
    public Guid FileId { get; init; }
}
