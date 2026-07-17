using FluentValidation;

namespace SDM.Application.Orders.UpdateOrderStatus;

/// <summary>
/// FluentValidation validator for <see cref="UpdateOrderStatusCommand"/>.
/// Invoked automatically by <c>ValidationBehavior</c> in the MediatR pipeline.
/// </summary>
public sealed class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    /// <summary>
    /// Initializes a new instance of <see cref="UpdateOrderStatusValidator"/>
    /// and defines all validation rules.
    /// </summary>
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required.");

        RuleFor(x => x.NewStatus)
            .IsInEnum().WithMessage("A valid order status must be provided.");

        When(x => x.Notes is not null, () =>
        {
            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.");
        });
    }
}
