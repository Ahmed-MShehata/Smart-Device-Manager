namespace SDM.API.Core;

/// <summary>
/// Defines the custom JWT claim type names used in the SDM system.
/// Complements <see cref="System.Security.Claims.ClaimTypes"/> with application-specific claims.
/// </summary>
public static class AppClaimTypes
{
    /// <summary>Claim containing the admin's username.</summary>
    public const string Username = "sdm_username";

    /// <summary>Claim containing the admin's unique identifier (Guid).</summary>
    public const string UserId = "sdm_userid";

    /// <summary>Claim containing the admin's role.</summary>
    public const string Role = "sdm_role";
}
