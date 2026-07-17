using SDM.Domain.Common;

namespace SDM.Domain.Entities;

public class AdminUser : BaseEntity
{
    public string Username { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = "Admin";
    public bool IsActive { get; private set; } = true;

    protected AdminUser() { }

    public AdminUser(string username, string passwordHash, string role = "Admin")
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
    public void UpdatePassword(string newHash) => PasswordHash = newHash;
}
