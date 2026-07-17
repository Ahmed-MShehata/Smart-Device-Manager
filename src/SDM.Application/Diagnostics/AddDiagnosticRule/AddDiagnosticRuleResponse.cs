namespace SDM.Application.Diagnostics.AddDiagnosticRule;

/// <summary>
/// Confirmation response after adding a rule.
/// </summary>
public sealed class AddDiagnosticRuleResponse
{
    /// <summary>Newly assigned rule ID.</summary>
    public Guid RuleId { get; init; }
}
