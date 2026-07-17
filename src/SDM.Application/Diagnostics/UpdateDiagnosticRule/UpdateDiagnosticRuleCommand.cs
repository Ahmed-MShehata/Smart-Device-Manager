using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.UpdateDiagnosticRule;

/// <summary>
/// Command to update an existing scoring rule.
/// </summary>
public sealed class UpdateDiagnosticRuleCommand : ICommand
{
    /// <summary>Gets the rule identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the lower bound of the score range (inclusive).</summary>
    public int MinScore { get; init; }

    /// <summary>Gets the upper bound of the score range (inclusive).</summary>
    public int MaxScore { get; init; }

    /// <summary>Gets the short diagnosis result label.</summary>
    public string Result { get; init; } = string.Empty;

    /// <summary>Gets the detailed step-by-step solution text.</summary>
    public string Solution { get; init; } = string.Empty;
}
