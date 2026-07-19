using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.KnowledgeBase.DeleteArticle;

/// <summary>Handles <see cref="DeleteArticleCommand"/>.</summary>
public sealed class DeleteArticleHandler : ICommandHandler<DeleteArticleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDb;

    public DeleteArticleHandler(IUnitOfWork unitOfWork, IReadDbContext readDb)
    {
        _unitOfWork = unitOfWork;
        _readDb = readDb;
    }

    public async Task<Result> Handle(
        DeleteArticleCommand command,
        CancellationToken cancellationToken)
    {
        var article = await _readDb.KnowledgeBaseArticles
            .Where(a => a.Id == command.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (article is null)
            return Result.Failure(Error.NotFound("KnowledgeBaseArticle"));

        _unitOfWork.KnowledgeBaseArticles.Remove(article);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Article deleted successfully.");
    }
}
