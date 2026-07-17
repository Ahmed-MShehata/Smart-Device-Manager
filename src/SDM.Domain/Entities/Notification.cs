using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a system-generated message addressed to customers, admins, or all users.
/// Used for order updates, system alerts, and admin broadcasts.
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>Gets the short heading of the notification.</summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>Gets the full body text of the notification.</summary>
    public string Message { get; private set; } = string.Empty;

    /// <summary>Gets the audience this notification is addressed to.</summary>
    public NotificationTarget Target { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this notification has been read.
    /// One-directional: can only transition from false to true.
    /// </summary>
    public bool IsRead { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this notification is pinned.
    /// Pinned notifications appear at the top of the list regardless of date.
    /// </summary>
    public bool IsPinned { get; private set; }

    /// <summary>
    /// Gets the device identifier this notification is targeted at.
    /// Null means the notification is a broadcast to all devices of the target type.
    /// </summary>
    public string? DeviceId { get; private set; }

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected Notification() { }

    /// <summary>
    /// Creates a new <see cref="Notification"/>.
    /// </summary>
    /// <param name="title">Short heading. Required.</param>
    /// <param name="message">Full body text. Required.</param>
    /// <param name="target">The audience for this notification.</param>
    /// <param name="deviceId">Optional device identifier for device-targeted notifications.</param>
    public Notification(
        string title,
        string message,
        NotificationTarget target,
        string? deviceId = null)
    {
        Title = title;
        Message = message;
        Target = target;
        DeviceId = deviceId;
        IsRead = false;
        IsPinned = false;
    }

    /// <summary>
    /// Marks this notification as read.
    /// This operation is one-directional — cannot be reversed.
    /// </summary>
    public void MarkAsRead() => IsRead = true;

    /// <summary>Pins this notification so it appears at the top of the list.</summary>
    public void Pin() => IsPinned = true;

    /// <summary>Unpins this notification.</summary>
    public void Unpin() => IsPinned = false;
}
