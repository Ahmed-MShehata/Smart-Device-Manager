namespace SDM.Domain.Enums;

/// <summary>Represents the availability status of a software package.</summary>
public enum PackageStatus
{
    /// <summary>Package is available and can be installed by customers.</summary>
    Active = 0,

    /// <summary>Package is not currently available.</summary>
    Inactive = 1,

    /// <summary>Package is deprecated and will not be sent to customers.</summary>
    Deprecated = 2
}
