using SDM.Application.Common.CQRS;

namespace SDM.Application.Notifications.MarkNotificationAsRead;

/// <summary>
/// Command to one-directionally transition a notification to 'Read'.
/// </summary>
public sealed class MarkNotificationAsReadCommand : ICommand
{
    /// <summary>Unique identifier. Driven from route.</summary>
    public Guid Id { get; init; }
}
