namespace SDM.Application.Diagnostics.CreateDiagnosticCategory;

/// <summary>
/// Response returned after a category is registered.
/// </summary>
public sealed class CreateDiagnosticCategoryResponse
{
    /// <summary>Newly assigned unique identifier.</summary>
    public Guid Id { get; init; }

    /// <summary>Display name.</summary>
    public string Name { get; init; } = string.Empty;
}
