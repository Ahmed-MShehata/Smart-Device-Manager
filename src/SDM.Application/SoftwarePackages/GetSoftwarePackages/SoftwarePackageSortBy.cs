namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>
/// Defines the fields by which the software package list can be sorted.
/// </summary>
public enum SoftwarePackageSortBy
{
    /// <summary>Sort by name (default).</summary>
    Name = 0,

    /// <summary>Sort by category.</summary>
    Category = 1,

    /// <summary>Sort by size.</summary>
    Size = 2,

    /// <summary>Sort by creation date.</summary>
    CreatedAt = 3
}
