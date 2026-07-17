using FluentValidation;

namespace SDM.Application.Diagnostics.AddDiagnosticRule;

/// <summary>
/// Validates <see cref="AddDiagnosticRuleCommand"/>.
/// </summary>
public sealed class AddDiagnosticRuleValidator : AbstractValidator<AddDiagnosticRuleCommand>
{
    /// <summary>Initializes a new instance of <see cref="AddDiagnosticRuleValidator"/>.</summary>
    public AddDiagnosticRuleValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.MinScore)
            .LessThanOrEqualTo(x => x.MaxScore).WithMessage("Minimum score cannot be greater than the maximum score.");

        RuleFor(x => x.Result)
            .NotEmpty().WithMessage("Diagnosis result label is required.")
            .MaximumLength(150).WithMessage("Result label must not exceed 150 characters.");

        RuleFor(x => x.Solution)
            .NotEmpty().WithMessage("Solution text is required.")
            .MaximumLength(2000).WithMessage("Solution text must not exceed 2000 characters.");
    }
}
