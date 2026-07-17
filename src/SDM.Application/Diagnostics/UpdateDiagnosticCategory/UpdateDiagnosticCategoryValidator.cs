using FluentValidation;

namespace SDM.Application.Diagnostics.UpdateDiagnosticCategory;

/// <summary>
/// Validates <see cref="UpdateDiagnosticCategoryCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticCategoryValidator : AbstractValidator<UpdateDiagnosticCategoryCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticCategoryValidator"/>.</summary>
    public UpdateDiagnosticCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.IconName)
            .MaximumLength(50).WithMessage("Icon name must not exceed 50 characters.");
    }
}
