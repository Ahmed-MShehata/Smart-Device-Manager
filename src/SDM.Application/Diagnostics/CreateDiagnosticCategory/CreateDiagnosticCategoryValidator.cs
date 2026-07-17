using FluentValidation;

namespace SDM.Application.Diagnostics.CreateDiagnosticCategory;

/// <summary>
/// Validates <see cref="CreateDiagnosticCategoryCommand"/>.
/// </summary>
public sealed class CreateDiagnosticCategoryValidator : AbstractValidator<CreateDiagnosticCategoryCommand>
{
    /// <summary>Initializes a new instance of <see cref="CreateDiagnosticCategoryValidator"/>.</summary>
    public CreateDiagnosticCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.IconName)
            .MaximumLength(50).WithMessage("Icon name must not exceed 50 characters.");
    }
}
