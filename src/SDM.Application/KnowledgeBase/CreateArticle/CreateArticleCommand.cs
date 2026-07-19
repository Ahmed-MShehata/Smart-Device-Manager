using SDM.Application.Common.CQRS;

namespace SDM.Application.KnowledgeBase.CreateArticle;

/// <summary>Command to create a new knowledge base troubleshooting article.</summary>
public sealed class CreateArticleCommand : ICommand<CreateArticleResponse>
{
    public string ProblemName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
    public string? ProblemImageUrl { get; init; }
    public string? YouTubeVideoUrl { get; init; }
    public bool Visible { get; init; } = true;
}
