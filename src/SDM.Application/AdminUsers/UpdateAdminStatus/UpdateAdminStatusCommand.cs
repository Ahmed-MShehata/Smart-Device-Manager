using SDM.Application.Common.CQRS;

namespace SDM.Application.AdminUsers.UpdateAdminStatus;

/// <summary>
/// Command to activate or deactivate an admin account.
/// Restricted to SuperAdmins only.
/// </summary>
public sealed class UpdateAdminStatusCommand : ICommand
{
    /// <summary>Gets the admin user identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the desired active state.</summary>
    public bool IsActive { get; init; }
}
