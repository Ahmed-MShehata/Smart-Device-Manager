using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a single answer option for a <see cref="DiagnosticQuestion"/>.
/// Carries a score value that contributes to the total score used to match a <see cref="DiagnosticRule"/>.
/// </summary>
public class DiagnosticChoice : BaseEntity
{
    /// <summary>Gets the identifier of the parent question.</summary>
    public Guid QuestionId { get; private set; }

    /// <summary>Gets the answer option text displayed to the customer.</summary>
    public string ChoiceText { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the score contribution of this answer.
    /// Can be any integer — negative values are valid as penalty scores.
    /// </summary>
    public int ScoreValue { get; private set; }

    // Navigation property
    /// <summary>Navigation property to the parent question. Populated by EF Core.</summary>
    public DiagnosticQuestion? Question { get; private set; }

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected DiagnosticChoice() { }

    /// <summary>
    /// Creates a new <see cref="DiagnosticChoice"/>.
    /// </summary>
    /// <param name="questionId">The Guid of the parent <see cref="DiagnosticQuestion"/>.</param>
    /// <param name="choiceText">Answer option text. Required.</param>
    /// <param name="scoreValue">Score contribution. Can be negative. Defaults to 0.</param>
    public DiagnosticChoice(Guid questionId, string choiceText, int scoreValue = 0)
    {
        QuestionId = questionId;
        ChoiceText = choiceText;
        ScoreValue = scoreValue;
    }

    /// <summary>Updates the choice text and score value.</summary>
    public void Update(string choiceText, int scoreValue)
    {
        ChoiceText = choiceText;
        ScoreValue = scoreValue;
    }
}
