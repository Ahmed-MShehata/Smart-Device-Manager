using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Application.KnowledgeBase.GetArticles;

namespace SDM.Application.KnowledgeBase.GetArticle;

/// <summary>Handles <see cref="GetArticleQuery"/>.</summary>
public sealed class GetArticleHandler : IQueryHandler<GetArticleQuery, ArticleDto>
{
    private readonly IReadDbContext _db;

    public GetArticleHandler(IReadDbContext db) => _db = db;

    public async Task<Result<ArticleDto>> Handle(GetArticleQuery query, CancellationToken cancellationToken)
    {
        var article = await _db.KnowledgeBaseArticles
            .Where(a => a.Id == query.Id)
            .Select(a => new ArticleDto
            {
                Id              = a.Id,
                ProblemName     = a.ProblemName,
                Description     = a.Description,
                ProblemImageUrl = a.ProblemImageUrl,
                YouTubeVideoUrl = a.YouTubeVideoUrl,
                Category        = a.Category,
                DisplayOrder    = a.DisplayOrder,
                Visible         = a.Visible,
                CreatedAt       = a.CreatedAt,
                UpdatedAt       = a.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (article is null)
            return Result<ArticleDto>.Failure(Error.NotFound("KnowledgeBaseArticle"));

        return Result<ArticleDto>.Success(article);
    }
}
