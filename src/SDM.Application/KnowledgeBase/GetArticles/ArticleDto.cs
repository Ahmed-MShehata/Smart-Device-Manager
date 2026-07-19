namespace SDM.Application.KnowledgeBase.GetArticles;

/// <summary>Article list item DTO.</summary>
public sealed class ArticleDto
{
    public Guid Id { get; init; }
    public string ProblemName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? ProblemImageUrl { get; init; }
    public string? YouTubeVideoUrl { get; init; }
    public string Category { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
    public bool Visible { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
