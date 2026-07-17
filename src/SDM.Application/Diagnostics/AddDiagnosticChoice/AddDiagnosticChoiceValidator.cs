using FluentValidation;

namespace SDM.Application.Diagnostics.AddDiagnosticChoice;

/// <summary>
/// Validates <see cref="AddDiagnosticChoiceCommand"/>.
/// </summary>
public sealed class AddDiagnosticChoiceValidator : AbstractValidator<AddDiagnosticChoiceCommand>
{
    /// <summary>Initializes a new instance of <see cref="AddDiagnosticChoiceValidator"/>.</summary>
    public AddDiagnosticChoiceValidator()
    {
        RuleFor(x => x.QuestionId).NotEmpty().WithMessage("Question ID is required.");

        RuleFor(x => x.ChoiceText)
            .NotEmpty().WithMessage("Choice text is required.")
            .MaximumLength(200).WithMessage("Choice text must not exceed 200 characters.");

        RuleFor(x => x.ScoreValue)
            .GreaterThanOrEqualTo(0).WithMessage("Score value must be zero or greater.");
    }
}
