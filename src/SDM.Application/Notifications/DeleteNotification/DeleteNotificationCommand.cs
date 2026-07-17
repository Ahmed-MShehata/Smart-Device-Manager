using SDM.Application.Common.CQRS;

namespace SDM.Application.Notifications.DeleteNotification;

/// <summary>
/// Command to hard delete an existing notification.
/// </summary>
public sealed class DeleteNotificationCommand : ICommand
{
    /// <summary>Unique identifier. Driven from route.</summary>
    public Guid Id { get; init; }
}
