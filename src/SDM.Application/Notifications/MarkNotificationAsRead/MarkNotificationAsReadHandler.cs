using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Notifications.MarkNotificationAsRead;

/// <summary>
/// Ensures the targeted notification is marked as read.
/// Only impacts unread notifications to avoid unnecessary DB commits.
/// </summary>
public sealed class MarkNotificationAsReadHandler : ICommandHandler<MarkNotificationAsReadCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="MarkNotificationAsReadHandler"/>.</summary>
    public MarkNotificationAsReadHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        MarkNotificationAsReadCommand command,
        CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.Notifications.GetByIdAsync(command.Id, cancellationToken);
        if (notification is null)
            return Result.Failure(Error.NotFound("Notification"));

        if (!notification.IsRead)
        {
            notification.MarkAsRead();
            _unitOfWork.Notifications.Update(notification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Success("Notification marked as read.");
    }
}
