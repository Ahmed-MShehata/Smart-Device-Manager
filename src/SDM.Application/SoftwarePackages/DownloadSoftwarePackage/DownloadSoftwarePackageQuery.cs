using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.DownloadSoftwarePackage;

/// <summary>Query to retrieve the download URL for a software package setup file.</summary>
public sealed class DownloadSoftwarePackageQuery : IQuery<DownloadSoftwarePackageResponse>
{
    public Guid Id { get; init; }
}
