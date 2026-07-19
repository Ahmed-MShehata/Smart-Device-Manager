using SDM.Application.Common.CQRS;

namespace SDM.Application.KnowledgeBase.DeleteArticle;

/// <summary>Command to permanently delete a knowledge base article.</summary>
public sealed class DeleteArticleCommand : ICommand
{
    public Guid Id { get; init; }
}
