using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.AddDiagnosticRule;

/// <summary>
/// Command to attach a new scoring rule to a diagnostic category.
/// </summary>
public sealed class AddDiagnosticRuleCommand : ICommand<AddDiagnosticRuleResponse>
{
    /// <summary>Gets the parent category identifier. Supplied by the route.</summary>
    public Guid CategoryId { get; init; }

    /// <summary>Gets the lower bound of the score range (inclusive).</summary>
    public int MinScore { get; init; }

    /// <summary>Gets the upper bound of the score range (inclusive).</summary>
    public int MaxScore { get; init; }

    /// <summary>Gets the short diagnosis result label.</summary>
    public string Result { get; init; } = string.Empty;

    /// <summary>Gets the detailed step-by-step solution text.</summary>
    public string Solution { get; init; } = string.Empty;
}
