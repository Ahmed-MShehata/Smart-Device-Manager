using SDM.Application.Common.CQRS;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackage;

/// <summary>
/// Query to retrieve full details of a specific software package by its ID.
/// </summary>
public sealed class GetSoftwarePackageQuery : IQuery<GetSoftwarePackageResponse>
{
    /// <summary>Gets the unique identifier of the package to retrieve.</summary>
    public Guid Id { get; init; }
}
