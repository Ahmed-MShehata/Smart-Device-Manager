namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>Fields by which the software package list can be sorted.</summary>
public enum SoftwarePackageSortBy
{
    /// <summary>Sort by display name (default).</summary>
    Name = 0,

    /// <summary>Sort by category (Application or Driver).</summary>
    Category = 1,

    /// <summary>Sort by creation date.</summary>
    CreatedAt = 2
}
