namespace SDM.API.Core;

/// <summary>
/// Defines the authorization policy names used in the API layer.
/// Policies are registered in <c>Program.cs</c> and referenced via <c>[Authorize(Policy = ...)]</c>.
/// </summary>
public static class Policies
{
    /// <summary>Requires the caller to be a SuperAdmin.</summary>
    public const string RequireSuperAdmin = "RequireSuperAdmin";

    /// <summary>Requires the caller to be an Admin or SuperAdmin.</summary>
    public const string RequireAdmin = "RequireAdmin";

    /// <summary>Requires the caller to be any authenticated admin (any role).</summary>
    public const string RequireAnyAdmin = "RequireAnyAdmin";
}
