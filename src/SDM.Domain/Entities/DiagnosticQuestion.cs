using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a single MCQ question within a <see cref="DiagnosticCategory"/>,
/// presented to the customer during offline diagnosis.
/// </summary>
public class DiagnosticQuestion : BaseEntity
{
    private readonly HashSet<DiagnosticChoice> _choices = [];

    /// <summary>Gets the identifier of the parent diagnostic category.</summary>
    public Guid CategoryId { get; private set; }

    /// <summary>Gets the question text displayed to the customer.</summary>
    public string QuestionText { get; private set; } = string.Empty;

    /// <summary>Gets the display order of this question within its category. Zero-based.</summary>
    public int OrderIndex { get; private set; }

    /// <summary>Gets the read-only collection of answer choices for this question. Must have at least 2.</summary>
    public IReadOnlyCollection<DiagnosticChoice> Choices => _choices;

    // Navigation property
    /// <summary>Navigation property to the parent category. Populated by EF Core.</summary>
    public DiagnosticCategory? Category { get; private set; }

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected DiagnosticQuestion() { }

    /// <summary>
    /// Creates a new <see cref="DiagnosticQuestion"/>.
    /// </summary>
    /// <param name="categoryId">The Guid of the parent <see cref="DiagnosticCategory"/>.</param>
    /// <param name="questionText">The question text. Required.</param>
    /// <param name="orderIndex">Display sequence index within the category. Defaults to 0.</param>
    public DiagnosticQuestion(Guid categoryId, string questionText, int orderIndex = 0)
    {
        CategoryId = categoryId;
        QuestionText = questionText;
        OrderIndex = orderIndex;
    }

    /// <summary>Adds an answer choice to this question.</summary>
    /// <param name="choice">The <see cref="DiagnosticChoice"/> to add.</param>
    public void AddChoice(DiagnosticChoice choice)
    {
        ArgumentNullException.ThrowIfNull(choice);
        _choices.Add(choice);
    }

    /// <summary>Updates the question text and display order.</summary>
    public void Update(string questionText, int orderIndex)
    {
        QuestionText = questionText;
        OrderIndex = orderIndex;
    }
}
