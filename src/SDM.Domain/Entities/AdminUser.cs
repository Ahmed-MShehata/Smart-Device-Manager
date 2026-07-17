using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents an administrator account used to access the Admin Desktop Application and Backend API.
/// Inherits audit fields from <see cref="AuditableEntity"/>.
/// </summary>
public class AdminUser : AuditableEntity
{
    /// <summary>Gets the unique login username for this administrator.</summary>
    public string Username { get; private set; } = string.Empty;

    /// <summary>Gets the hashed password. Never stores plain-text passwords.</summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>Gets the role that defines the administrator's access level.</summary>
    public AdminRole Role { get; private set; } = AdminRole.Admin;

    /// <summary>Gets a value indicating whether this account is allowed to authenticate.</summary>
    public bool IsActive { get; private set; } = true;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected AdminUser() { }

    /// <summary>
    /// Creates a new <see cref="AdminUser"/>.
    /// </summary>
    /// <param name="username">Unique login identifier. MinLength: 3, MaxLength: 50.</param>
    /// <param name="passwordHash">Pre-hashed password string. Never pass plain text.</param>
    /// <param name="createdBy">Username of the admin who created this account.</param>
    /// <param name="role">Access level role. Defaults to <see cref="AdminRole.Admin"/>.</param>
    public AdminUser(string username, string passwordHash, string createdBy, AdminRole role = AdminRole.Admin)
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
        CreatedBy = createdBy;
    }

    /// <summary>Deactivates this account, preventing authentication.</summary>
    /// <param name="updatedBy">Username of the admin performing the action.</param>
    public void Deactivate(string updatedBy)
    {
        IsActive = false;
        RecordUpdate(updatedBy);
    }

    /// <summary>Reactivates this account, allowing authentication.</summary>
    /// <param name="updatedBy">Username of the admin performing the action.</param>
    public void Activate(string updatedBy)
    {
        IsActive = true;
        RecordUpdate(updatedBy);
    }

    /// <summary>Replaces the stored password hash.</summary>
    /// <param name="newPasswordHash">The new hashed password. Never pass plain text.</param>
    /// <param name="updatedBy">Username of the admin performing the action.</param>
    public void UpdatePassword(string newPasswordHash, string updatedBy)
    {
        PasswordHash = newPasswordHash;
        RecordUpdate(updatedBy);
    }

    /// <summary>Changes the role of this administrator.</summary>
    /// <param name="newRole">The new <see cref="AdminRole"/> to assign.</param>
    /// <param name="updatedBy">Username of the admin performing the action.</param>
    public void UpdateRole(AdminRole newRole, string updatedBy)
    {
        Role = newRole;
        RecordUpdate(updatedBy);
    }
}
