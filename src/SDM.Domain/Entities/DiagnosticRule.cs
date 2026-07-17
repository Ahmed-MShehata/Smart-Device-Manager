using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a scoring rule within a <see cref="DiagnosticCategory"/>.
/// Maps a total MCQ score range to a human-readable diagnosis result and solution.
/// </summary>
public class DiagnosticRule : BaseEntity
{
    /// <summary>Gets the identifier of the owning diagnostic category.</summary>
    public Guid CategoryId { get; private set; }

    /// <summary>Gets the lower bound (inclusive) of the score range that triggers this rule.</summary>
    public int MinScore { get; private set; }

    /// <summary>Gets the upper bound (inclusive) of the score range that triggers this rule.</summary>
    public int MaxScore { get; private set; }

    /// <summary>Gets the short diagnosis result label (e.g., "Overheating — Critical").</summary>
    public string Result { get; private set; } = string.Empty;

    /// <summary>Gets the detailed step-by-step solution text shown to the customer.</summary>
    public string Solution { get; private set; } = string.Empty;

    // Navigation property
    /// <summary>Navigation property to the owning category. Populated by EF Core.</summary>
    public DiagnosticCategory? Category { get; private set; }

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected DiagnosticRule() { }

    /// <summary>
    /// Creates a new <see cref="DiagnosticRule"/>.
    /// </summary>
    /// <param name="categoryId">The Guid of the owning <see cref="DiagnosticCategory"/>.</param>
    /// <param name="minScore">Lower bound of the score range. Must be less than or equal to maxScore.</param>
    /// <param name="maxScore">Upper bound of the score range. Must be greater than or equal to minScore.</param>
    /// <param name="result">Short diagnosis result label. Required.</param>
    /// <param name="solution">Detailed solution steps. Required.</param>
    public DiagnosticRule(Guid categoryId, int minScore, int maxScore, string result, string solution)
    {
        CategoryId = categoryId;
        MinScore = minScore;
        MaxScore = maxScore;
        Result = result;
        Solution = solution;
    }

    /// <summary>Updates the score range, result, and solution for this rule.</summary>
    public void Update(int minScore, int maxScore, string result, string solution)
    {
        MinScore = minScore;
        MaxScore = maxScore;
        Result = result;
        Solution = solution;
    }
}
