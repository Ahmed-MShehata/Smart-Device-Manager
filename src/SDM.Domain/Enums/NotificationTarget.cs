namespace SDM.Domain.Enums;

/// <summary>Defines the audience that a notification is addressed to.</summary>
public enum NotificationTarget
{
    /// <summary>Notification is sent to customers only.</summary>
    Customer = 0,

    /// <summary>Notification is sent to administrators only.</summary>
    Admin = 1,

    /// <summary>Notification is broadcast to all users.</summary>
    All = 2
}
