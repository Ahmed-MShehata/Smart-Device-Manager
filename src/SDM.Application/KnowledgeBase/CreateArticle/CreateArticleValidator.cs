using FluentValidation;

namespace SDM.Application.KnowledgeBase.CreateArticle;

/// <summary>FluentValidation rules for <see cref="CreateArticleCommand"/>.</summary>
public sealed class CreateArticleValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleValidator()
    {
        RuleFor(x => x.ProblemName)
            .NotEmpty().WithMessage("Problem name is required.")
            .MaximumLength(300).WithMessage("Problem name must not exceed 300 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(10000).WithMessage("Description must not exceed 10000 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Display order must be zero or greater.");
    }
}
