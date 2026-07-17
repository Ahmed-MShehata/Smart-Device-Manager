namespace SDM.Domain.Common;

/// <summary>
/// Extends <see cref="BaseEntity"/> with audit trail fields.
/// Used by entities that are actively managed via CRUD operations.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>Gets the UTC date and time when this entity was last updated. Null if never updated.</summary>
    public DateTime? UpdatedAt { get; protected set; }

    /// <summary>Gets the username of the admin who created this record.</summary>
    public string CreatedBy { get; protected set; } = string.Empty;

    /// <summary>Gets the username of the admin who last modified this record. Null if never modified.</summary>
    public string? UpdatedBy { get; protected set; }

    /// <summary>
    /// Records an update to this entity, setting <see cref="UpdatedAt"/> and <see cref="UpdatedBy"/>.
    /// </summary>
    /// <param name="updatedBy">The username of the admin performing the update.</param>
    protected void RecordUpdate(string updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}
