using SDM.Application.Common.CQRS;

namespace SDM.Application.KnowledgeBase.UpdateArticle;

/// <summary>Command to update an existing knowledge base article.</summary>
public sealed class UpdateArticleCommand : ICommand
{
    /// <summary>Gets the article ID to update. Supplied by the route.</summary>
    public Guid Id { get; init; }

    public string ProblemName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
    public string? ProblemImageUrl { get; init; }
    public string? YouTubeVideoUrl { get; init; }
    public bool Visible { get; init; } = true;
}
