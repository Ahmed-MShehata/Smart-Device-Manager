using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.Notifications.CreateNotification;

/// <summary>
/// Command to publish a new system notification.
/// </summary>
public sealed class CreateNotificationCommand : ICommand<CreateNotificationResponse>
{
    /// <summary>Gets the notification heading. Required.</summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>Gets the full notification body. Required.</summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>Gets the targeted audience for the notification.</summary>
    public NotificationTarget Target { get; init; }

    /// <summary>
    /// Gets the target device identifier. Required only if <see cref="Target"/> is Device.
    /// Otherwise, should be null for broadcast messages.
    /// </summary>
    public string? DeviceId { get; init; }

    /// <summary>Gets a value indicating whether this instruction should immediately be pinned.</summary>
    public bool Pin { get; init; }
}
