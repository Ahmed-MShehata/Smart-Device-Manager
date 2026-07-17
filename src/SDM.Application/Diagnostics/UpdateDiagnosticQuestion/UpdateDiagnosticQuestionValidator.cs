using FluentValidation;

namespace SDM.Application.Diagnostics.UpdateDiagnosticQuestion;

/// <summary>
/// Validates <see cref="UpdateDiagnosticQuestionCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticQuestionValidator : AbstractValidator<UpdateDiagnosticQuestionCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticQuestionValidator"/>.</summary>
    public UpdateDiagnosticQuestionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Question ID is required.");

        RuleFor(x => x.QuestionText)
            .NotEmpty().WithMessage("Question text is required.")
            .MaximumLength(500).WithMessage("Question text must not exceed 500 characters.");

        RuleFor(x => x.OrderIndex)
            .GreaterThanOrEqualTo(0).WithMessage("Order index must be zero or greater.");
    }
}
