namespace SDM.Domain.Enums;

/// <summary>Represents the availability status of a system component.</summary>
public enum ComponentStatus
{
    /// <summary>Component is active and can be deployed to customers.</summary>
    Active = 0,

    /// <summary>Component is inactive and will not be deployed.</summary>
    Inactive = 1
}
