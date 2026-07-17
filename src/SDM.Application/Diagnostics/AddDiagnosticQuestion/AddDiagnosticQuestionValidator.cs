using FluentValidation;

namespace SDM.Application.Diagnostics.AddDiagnosticQuestion;

/// <summary>
/// Validates <see cref="AddDiagnosticQuestionCommand"/>.
/// </summary>
public sealed class AddDiagnosticQuestionValidator : AbstractValidator<AddDiagnosticQuestionCommand>
{
    /// <summary>Initializes a new instance of <see cref="AddDiagnosticQuestionValidator"/>.</summary>
    public AddDiagnosticQuestionValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.QuestionText)
            .NotEmpty().WithMessage("Question text is required.")
            .MaximumLength(500).WithMessage("Question text must not exceed 500 characters.");

        RuleFor(x => x.OrderIndex)
            .GreaterThanOrEqualTo(0).WithMessage("Order index must be zero or greater.");

        RuleFor(x => x.Choices)
            .NotEmpty().WithMessage("At least one choice is required.")
            .Must(c => c.Count >= 2).WithMessage("Each question must have at least 2 choices.");

        RuleForEach(x => x.Choices).ChildRules(choice =>
        {
            choice.RuleFor(c => c.ChoiceText)
                .NotEmpty().WithMessage("Choice text is required.")
                .MaximumLength(300).WithMessage("Choice text must not exceed 300 characters.");
        });
    }
}
