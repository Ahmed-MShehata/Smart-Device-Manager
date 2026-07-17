using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.UpdateDiagnosticCategory;

/// <summary>
/// Command to update the editable metadata of a diagnostic category.
/// </summary>
public sealed class UpdateDiagnosticCategoryCommand : ICommand
{
    /// <summary>Gets the unique identifier. Supplied by the route.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the new display name.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the updated description.</summary>
    public string? Description { get; init; }

    /// <summary>Gets the updated icon name.</summary>
    public string? IconName { get; init; }
}
