using FluentValidation;

namespace SDM.Application.Products.CreateProduct;

/// <summary>
/// FluentValidation validator for <see cref="CreateProductCommand"/>.
/// Enforces all business input rules before the command reaches <see cref="CreateProductHandler"/>.
/// Invoked automatically by <c>ValidationBehavior</c> in the MediatR pipeline.
/// </summary>
public sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>Initializes a new instance of <see cref="CreateProductValidator"/> and defines all rules.</summary>
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Product name is required.")
            .MaximumLength(200)
                .WithMessage("Product name must not exceed 200 characters.");

        RuleFor(x => x.Category)
            .IsInEnum()
                .WithMessage("A valid product category must be provided.");

        RuleFor(x => x.Brand)
            .NotEmpty()
                .WithMessage("Brand is required.")
            .MaximumLength(100)
                .WithMessage("Brand must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage("Description is required.")
            .MaximumLength(2000)
                .WithMessage("Description must not exceed 2000 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Discount)
            .InclusiveBetween(0m, 100m)
                .WithMessage("Discount must be between 0 and 100.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative.");

        RuleFor(x => x.WarrantyMonths)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Warranty months cannot be negative.");

        When(x => x.ImagePath is not null, () =>
        {
            RuleFor(x => x.ImagePath)
                .MaximumLength(500)
                    .WithMessage("Image path must not exceed 500 characters.");
        });
    }
}
