using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.KnowledgeBase.GetArticles;

/// <summary>Handles <see cref="GetArticlesQuery"/>.</summary>
public sealed class GetArticlesHandler : IQueryHandler<GetArticlesQuery, List<ArticleDto>>
{
    private readonly IReadDbContext _db;

    public GetArticlesHandler(IReadDbContext db) => _db = db;

    public async Task<Result<List<ArticleDto>>> Handle(
        GetArticlesQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.KnowledgeBaseArticles.AsQueryable();

        if (!query.IncludeHidden)
            q = q.Where(a => a.Visible);

        if (!string.IsNullOrWhiteSpace(query.Category))
            q = q.Where(a => a.Category == query.Category);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            q = q.Where(a =>
                EF.Functions.Like(a.ProblemName, $"%{query.Search}%") ||
                EF.Functions.Like(a.Description, $"%{query.Search}%"));
        }

        var articles = await q
            .OrderBy(a => a.DisplayOrder)
            .ThenBy(a => a.ProblemName)
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
            .ToListAsync(cancellationToken);

        return Result<List<ArticleDto>>.Success(articles);
    }
}
