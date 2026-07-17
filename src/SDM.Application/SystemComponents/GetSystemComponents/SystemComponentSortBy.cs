namespace SDM.Application.SystemComponents.GetSystemComponents;

/// <summary>
/// Defines the fields by which the system component list can be sorted.
/// </summary>
public enum SystemComponentSortBy
{
    /// <summary>Sort by name (default).</summary>
    Name = 0,

    /// <summary>Sort by size.</summary>
    Size = 1,

    /// <summary>Sort by creation date.</summary>
    CreatedAt = 2
}
