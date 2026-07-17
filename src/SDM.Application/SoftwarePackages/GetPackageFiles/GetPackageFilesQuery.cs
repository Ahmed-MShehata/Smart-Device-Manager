using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.GetPackageFiles;

/// <summary>
/// Query to retrieve all file metadata records for a specific software package.
/// </summary>
public sealed class GetPackageFilesQuery : IQuery<List<PackageFileRecord>>
{
    /// <summary>Gets the parent package identifier.</summary>
    public Guid PackageId { get; init; }
}
