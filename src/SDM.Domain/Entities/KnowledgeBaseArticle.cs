using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a troubleshooting guide managed by the admin and displayed
/// in the Customer Application Knowledge Base.
///
/// Admin creates/edits articles. Customers browse visible articles only.
/// YouTube video URL opens the system browser when tapped.
/// Display order controls sort position.
/// </summary>
public class KnowledgeBaseArticle : AuditableEntity
{
    /// <summary>Gets the title of the problem this article addresses.</summary>
    public string ProblemName { get; private set; } = string.Empty;

    /// <summary>Gets the full body text of the article.</summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>Gets the server-side path to the problem image. Null if not uploaded.</summary>
    public string? ProblemImageUrl { get; private set; }

    /// <summary>Gets the YouTube video URL linked to this article. Null if none.</summary>
    public string? YouTubeVideoUrl { get; private set; }

    /// <summary>Gets the category grouping for this article (e.g., "Drivers", "Network").</summary>
    public string Category { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the display order position.
    /// Lower values appear first in the customer list.
    /// </summary>
    public int DisplayOrder { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this article is visible to customers.
    /// Hidden articles are managed from the admin panel only.
    /// </summary>
    public bool Visible { get; private set; } = true;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected KnowledgeBaseArticle() { }

    /// <summary>
    /// Creates a new <see cref="KnowledgeBaseArticle"/>.
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    public KnowledgeBaseArticle(
        string problemName,
        string description,
        string category,
        int displayOrder,
        string? problemImageUrl = null,
        string? youTubeVideoUrl = null,
        bool visible = true)
    {
        ProblemName = problemName;
        Description = description;
        Category = category;
        DisplayOrder = displayOrder;
        ProblemImageUrl = problemImageUrl;
        YouTubeVideoUrl = youTubeVideoUrl;
        Visible = visible;
    }

    /// <summary>Updates all editable fields of this article.</summary>
    public void Update(
        string problemName,
        string description,
        string category,
        int displayOrder,
        string? problemImageUrl,
        string? youTubeVideoUrl)
    {
        ProblemName = problemName;
        Description = description;
        Category = category;
        DisplayOrder = displayOrder;
        ProblemImageUrl = problemImageUrl;
        YouTubeVideoUrl = youTubeVideoUrl;
    }

    /// <summary>Sets the visibility of this article.</summary>
    public void SetVisibility(bool visible) => Visible = visible;
}
