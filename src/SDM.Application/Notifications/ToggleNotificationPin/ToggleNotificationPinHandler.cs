using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Notifications.ToggleNotificationPin;

/// <summary>
/// Inverts whether a notification sits at the top of the feed or flows naturally with time.
/// </summary>
public sealed class ToggleNotificationPinHandler : ICommandHandler<ToggleNotificationPinCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="ToggleNotificationPinHandler"/>.</summary>
    public ToggleNotificationPinHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        ToggleNotificationPinCommand command,
        CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.Notifications.GetByIdAsync(command.Id, cancellationToken);
        if (notification is null)
            return Result.Failure(Error.NotFound("Notification"));

        if (notification.IsPinned)
            notification.Unpin();
        else
            notification.Pin();

        _unitOfWork.Notifications.Update(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(notification.IsPinned ? "Notification pinned." : "Notification unpinned.");
    }
}
