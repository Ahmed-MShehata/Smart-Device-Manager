using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.DeleteDiagnosticQuestion;

/// <summary>
/// Command to remove an existing MCQ question completely from the tree.
/// </summary>
public sealed class DeleteDiagnosticQuestionCommand : ICommand
{
    /// <summary>Gets the question identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }
}
