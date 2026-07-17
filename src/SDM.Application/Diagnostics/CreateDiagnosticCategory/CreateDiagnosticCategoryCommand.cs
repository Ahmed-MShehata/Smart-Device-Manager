using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.CreateDiagnosticCategory;

/// <summary>
/// Command to register a new top-level diagnostic problem category.
/// </summary>
public sealed class CreateDiagnosticCategoryCommand : ICommand<CreateDiagnosticCategoryResponse>
{
    /// <summary>Gets the unique display name. Required.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets an optional description of the problem area.</summary>
    public string? Description { get; init; }

    /// <summary>Gets an optional UI icon identifier for this category.</summary>
    public string? IconName { get; init; }
}
