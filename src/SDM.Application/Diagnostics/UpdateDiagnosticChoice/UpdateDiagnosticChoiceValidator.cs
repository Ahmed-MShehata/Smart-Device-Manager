using FluentValidation;

namespace SDM.Application.Diagnostics.UpdateDiagnosticChoice;

/// <summary>
/// Validates <see cref="UpdateDiagnosticChoiceCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticChoiceValidator : AbstractValidator<UpdateDiagnosticChoiceCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticChoiceValidator"/>.</summary>
    public UpdateDiagnosticChoiceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Choice ID is required.");

        RuleFor(x => x.ChoiceText)
            .NotEmpty().WithMessage("Choice text is required.")
            .MaximumLength(200).WithMessage("Choice text must not exceed 200 characters.");

        RuleFor(x => x.ScoreValue)
            .GreaterThanOrEqualTo(0).WithMessage("Score value must be zero or greater.");
    }
}
