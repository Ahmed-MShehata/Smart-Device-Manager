using SDM.Domain.Enums;

namespace SDM.Application.Notifications.GetNotifications;

/// <summary>
/// Full notification payload representation optimized for lists.
/// </summary>
public sealed class GetNotificationsResponse
{
    /// <summary>Database unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Heading.</summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>Body paragraph.</summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>Has it been interacted with?</summary>
    public bool IsRead { get; init; }

    /// <summary>Should the UI pin it arbitrarily high?</summary>
    public bool IsPinned { get; init; }

    /// <summary>Device lock-in reference.</summary>
    public string? DeviceId { get; init; }

    /// <summary>Determines context (Admin alert vs Customer notice).</summary>
    public NotificationTarget Target { get; init; }

    /// <summary>Published timeline reference.</summary>
    public DateTime CreatedAt { get; init; }
}
