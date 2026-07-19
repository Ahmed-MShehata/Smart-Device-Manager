using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.KnowledgeBase.UpdateArticle;

/// <summary>Handles <see cref="UpdateArticleCommand"/>.</summary>
public sealed class UpdateArticleHandler : ICommandHandler<UpdateArticleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateArticleHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
    {
        var article = await _unitOfWork.KnowledgeBaseArticles.GetByIdAsync(command.Id, cancellationToken);

        if (article is null)
            return Result.Failure(Error.NotFound("KnowledgeBaseArticle"));

        article.Update(
            command.ProblemName,
            command.Description,
            command.Category,
            command.DisplayOrder,
            command.ProblemImageUrl,
            command.YouTubeVideoUrl);

        article.SetVisibility(command.Visible);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Article updated successfully.");
    }
}
