using FluentValidation;

namespace SDM.Application.Notifications.GetNotifications;

/// <summary>
/// Validates <see cref="GetNotificationsQuery"/>.
/// </summary>
public sealed class GetNotificationsValidator : AbstractValidator<GetNotificationsQuery>
{
    /// <summary>Initializes a new instance of <see cref="GetNotificationsValidator"/>.</summary>
    public GetNotificationsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

        RuleFor(x => x.Target)
            .IsInEnum().WithMessage("A valid target audience must be selected.");
    }
}
