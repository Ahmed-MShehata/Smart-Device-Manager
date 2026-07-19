using SDM.Application.Common.CQRS;

namespace SDM.Application.AdminUsers.DeleteAdminUser;

/// <summary>
/// Command to soft-delete an admin account by deactivating it.
/// The record is never physically removed from the database.
/// Restricted to SuperAdmins only.
/// </summary>
public sealed class DeleteAdminUserCommand : ICommand
{
    /// <summary>Gets the admin user ID to deactivate. Supplied by the route.</summary>
    public Guid Id { get; init; }
}
