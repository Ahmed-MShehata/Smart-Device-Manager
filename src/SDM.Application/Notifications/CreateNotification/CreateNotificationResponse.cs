namespace SDM.Application.Notifications.CreateNotification;

/// <summary>
/// Response confirmation after dispatching a notification.
/// </summary>
public sealed class CreateNotificationResponse
{
    /// <summary>Unique identifier of the published notification.</summary>
    public Guid Id { get; init; }

    /// <summary>The UTC timestamp when it was published.</summary>
    public DateTime CreatedAt { get; init; }
}
