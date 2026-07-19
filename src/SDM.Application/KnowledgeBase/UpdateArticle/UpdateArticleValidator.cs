using FluentValidation;

namespace SDM.Application.KnowledgeBase.UpdateArticle;

/// <summary>Validates <see cref="UpdateArticleCommand"/>.</summary>
public sealed class UpdateArticleValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Article ID is required.");

        RuleFor(x => x.ProblemName)
            .NotEmpty().WithMessage("Problem name is required.")
            .MaximumLength(200).WithMessage("Problem name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Display order must be 0 or greater.");

        RuleFor(x => x.YouTubeVideoUrl)
            .MaximumLength(500).WithMessage("YouTube video URL must not exceed 500 characters.")
            .When(x => x.YouTubeVideoUrl is not null);
    }
}
