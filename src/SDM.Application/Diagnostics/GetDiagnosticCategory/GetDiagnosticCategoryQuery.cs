using SDM.Application.Common.CQRS;

namespace SDM.Application.Diagnostics.GetDiagnosticCategory;

/// <summary>
/// Query to retrieve full hierarchical details (category + questions + choices + rules) for a single diagnostic category.
/// </summary>
public sealed class GetDiagnosticCategoryQuery : IQuery<GetDiagnosticCategoryResponse>
{
    /// <summary>Gets the unique identifier of the category to retrieve.</summary>
    public Guid Id { get; init; }
}
