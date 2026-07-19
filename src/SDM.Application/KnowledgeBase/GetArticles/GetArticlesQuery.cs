using SDM.Application.Common.CQRS;

namespace SDM.Application.KnowledgeBase.GetArticles;

/// <summary>Query to retrieve a list of knowledge base articles.</summary>
public sealed class GetArticlesQuery : IQuery<List<ArticleDto>>
{
    /// <summary>When true, returns all articles including hidden ones (admin view).</summary>
    public bool IncludeHidden { get; init; } = false;

    /// <summary>Optional category filter.</summary>
    public string? Category { get; init; }
}
