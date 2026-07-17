using SDM.Application.Common.CQRS;

namespace SDM.Application.Notifications.ToggleNotificationPin;

/// <summary>
/// Command to reverse the pin status of a notification.
/// </summary>
public sealed class ToggleNotificationPinCommand : ICommand
{
    /// <summary>Unique identifier. Driven from route.</summary>
    public Guid Id { get; init; }
}
