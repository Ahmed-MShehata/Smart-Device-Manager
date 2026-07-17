using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Notifications.CreateNotification;

/// <summary>
/// Handles <see cref="CreateNotificationCommand"/> and persists the generated object.
/// </summary>
public sealed class CreateNotificationHandler : ICommandHandler<CreateNotificationCommand, CreateNotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="CreateNotificationHandler"/>.</summary>
    public CreateNotificationHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<CreateNotificationResponse>> Handle(
        CreateNotificationCommand command,
        CancellationToken cancellationToken)
    {
        var notification = new Notification(
            command.Title,
            command.Message,
            command.Target,
            command.DeviceId);

        if (command.Pin)
            notification.Pin();

        await _unitOfWork.Notifications.AddAsync(notification, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateNotificationResponse>.Success(
            new CreateNotificationResponse { Id = notification.Id, CreatedAt = notification.CreatedAt },
            "Notification created successfully.");
    }
}
