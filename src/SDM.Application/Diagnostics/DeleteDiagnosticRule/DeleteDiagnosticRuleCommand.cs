using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.DeleteDiagnosticRule;

/// <summary>
/// Command to remove an existing scoring rule.
/// </summary>
public sealed class DeleteDiagnosticRuleCommand : ICommand
{
    /// <summary>Gets the rule identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }
}
