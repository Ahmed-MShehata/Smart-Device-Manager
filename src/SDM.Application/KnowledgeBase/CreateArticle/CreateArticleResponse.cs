namespace SDM.Application.KnowledgeBase.CreateArticle;

/// <summary>Response returned after successfully creating a knowledge base article.</summary>
public sealed class CreateArticleResponse
{
    public Guid Id { get; init; }
    public string ProblemName { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public bool Visible { get; init; }
    public DateTime CreatedAt { get; init; }
}
