namespace SDM.API.Core;

/// <summary>
/// Defines the role names used throughout the application for authorization.
/// These must match the role values stored in the JWT and in <see cref="SDM.Domain.Enums.AdminRole"/>.
/// </summary>
public static class Roles
{
    /// <summary>Root administrator with full system access.</summary>
    public const string SuperAdmin = "SuperAdmin";

    /// <summary>Standard administrator with access to most features.</summary>
    public const string Admin = "Admin";

    /// <summary>Support role with limited read access.</summary>
    public const string Support = "Support";
}
