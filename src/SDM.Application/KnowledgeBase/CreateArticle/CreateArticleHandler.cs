using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.KnowledgeBase.CreateArticle;

/// <summary>Handles <see cref="CreateArticleCommand"/>.</summary>
public sealed class CreateArticleHandler : ICommandHandler<CreateArticleCommand, CreateArticleResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateArticleHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<CreateArticleResponse>> Handle(
        CreateArticleCommand command,
        CancellationToken cancellationToken)
    {
        var article = new KnowledgeBaseArticle(
            command.ProblemName,
            command.Description,
            command.Category,
            command.DisplayOrder,
            command.ProblemImageUrl,
            command.YouTubeVideoUrl,
            command.Visible);

        await _unitOfWork.KnowledgeBaseArticles.AddAsync(article, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateArticleResponse>.Success(
            new CreateArticleResponse
            {
                Id          = article.Id,
                ProblemName = article.ProblemName,
                Category    = article.Category,
                Visible     = article.Visible,
                CreatedAt   = article.CreatedAt
            },
            "Article created successfully.");
    }
}
