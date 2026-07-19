using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents an administrator account used to access the Admin Desktop Application and Backend API.
/// Audit fields (<c>CreatedBy</c>, <c>UpdatedBy</c>, <c>UpdatedAt</c>) are stamped
/// automatically by Infrastructure — never by this entity.
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
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    /// <param name="username">Unique login identifier. MinLength: 3, MaxLength: 50.</param>
    /// <param name="passwordHash">Pre-hashed password string. Never pass plain text.</param>
    /// <param name="role">Access level role. Defaults to <see cref="AdminRole.Admin"/>.</param>
    public AdminUser(string username, string passwordHash, AdminRole role = AdminRole.Admin)
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    /// <summary>Deactivates this account, preventing authentication.</summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>Reactivates this account, allowing authentication.</summary>
    public void Activate()
    {
        IsActive = true;
    }

    /// <summary>Replaces the stored password hash.</summary>
    /// <param name="newPasswordHash">The new hashed password. Never pass plain text.</param>
    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    /// <summary>Changes the role of this administrator.</summary>
    /// <param name="newRole">The new <see cref="AdminRole"/> to assign.</param>
    public void UpdateRole(AdminRole newRole)
    {
        Role = newRole;
    }

    /// <summary>Changes the login username of this administrator.</summary>
    /// <param name="newUsername">The new unique username. Checked for uniqueness in the handler.</param>
    public void UpdateUsername(string newUsername)
    {
        Username = newUsername;
    }
}
