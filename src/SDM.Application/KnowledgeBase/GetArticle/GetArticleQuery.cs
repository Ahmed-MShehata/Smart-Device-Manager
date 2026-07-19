using SDM.Application.Common.CQRS;
using SDM.Application.KnowledgeBase.GetArticles;

namespace SDM.Application.KnowledgeBase.GetArticle;

/// <summary>Query to retrieve a single knowledge base article by ID.</summary>
public sealed class GetArticleQuery : IQuery<ArticleDto>
{
    public Guid Id { get; init; }
}
