using FluentValidation;
using SDM.Domain.Enums;

namespace SDM.Application.Notifications.CreateNotification;

/// <summary>
/// Validates <see cref="CreateNotificationCommand"/>.
/// </summary>
public sealed class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
{
    /// <summary>Initializes a new instance of <see cref="CreateNotificationValidator"/>.</summary>
    public CreateNotificationValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message body is required.")
            .MaximumLength(2000).WithMessage("Message body must not exceed 2000 characters.");

        RuleFor(x => x.Target)
            .IsInEnum().WithMessage("A valid target must be selected.");

        RuleFor(x => x.DeviceId)
            .MaximumLength(100).WithMessage("Device ID must not exceed 100 characters.");
    }
}
