using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Notifications.DeleteNotification;

/// <summary>
/// Handles permanent erasure of notifications.
/// </summary>
public sealed class DeleteNotificationHandler : ICommandHandler<DeleteNotificationCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="DeleteNotificationHandler"/>.</summary>
    public DeleteNotificationHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        DeleteNotificationCommand command,
        CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.Notifications.GetByIdAsync(command.Id, cancellationToken);
        if (notification is null)
            return Result.Failure(Error.NotFound("Notification"));

        _unitOfWork.Notifications.Remove(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Notification removed permanently.");
    }
}
