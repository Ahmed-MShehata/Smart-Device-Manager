using FluentValidation;

namespace SDM.Application.Diagnostics.UpdateDiagnosticRule;

/// <summary>
/// Validates <see cref="UpdateDiagnosticRuleCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticRuleValidator : AbstractValidator<UpdateDiagnosticRuleCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticRuleValidator"/>.</summary>
    public UpdateDiagnosticRuleValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Rule ID is required.");

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
