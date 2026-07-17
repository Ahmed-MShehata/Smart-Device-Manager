namespace SDM.Domain.Enums;

/// <summary>Defines the access level roles for administrator accounts.</summary>
public enum AdminRole
{
    /// <summary>Full system access. Can manage other admins.</summary>
    SuperAdmin = 0,

    /// <summary>Standard administrative access.</summary>
    Admin = 1,

    /// <summary>Limited access for support staff.</summary>
    Support = 2
}
