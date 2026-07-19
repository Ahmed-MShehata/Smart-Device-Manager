using FluentValidation;

namespace SDM.Application.Orders.CreateOrder;

/// <summary>
/// FluentValidation validator for <see cref="CreateOrderCommand"/>.
/// Invoked automatically by ValidationBehavior in the MediatR pipeline.
/// </summary>
public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters.");

        RuleFor(x => x.CustomerPhone)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(30).WithMessage("Phone number must not exceed 30 characters.");

        When(x => x.CustomerWhatsApp is not null, () =>
        {
            RuleFor(x => x.CustomerWhatsApp)
                .MaximumLength(30).WithMessage("WhatsApp number must not exceed 30 characters.");
        });

        RuleFor(x => x.CustomerGovernorate)
            .NotEmpty().WithMessage("Governorate is required.")
            .MaximumLength(100).WithMessage("Governorate must not exceed 100 characters.");

        RuleFor(x => x.CustomerAddress)
            .NotEmpty().WithMessage("Delivery address is required.")
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one order item is required.");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("Product ID is required for each order item.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");
        });
    }
}
