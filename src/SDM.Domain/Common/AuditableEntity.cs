namespace SDM.Domain.Common;

/// <summary>
/// Extends <see cref="BaseEntity"/> with audit trail fields.
/// Audit values (<see cref="CreatedBy"/>, <see cref="UpdatedBy"/>, <see cref="UpdatedAt"/>)
/// are automatically stamped by the Infrastructure layer during <c>SaveChangesAsync</c>.
/// Domain entities do not set these fields directly.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>Gets the UTC date and time when this entity was last updated. Null if never updated.</summary>
    public DateTime? UpdatedAt { get; protected set; }

    /// <summary>Gets the username of the admin who created this record.</summary>
    public string CreatedBy { get; protected set; } = string.Empty;

    /// <summary>Gets the username of the admin who last modified this record. Null if never modified.</summary>
    public string? UpdatedBy { get; protected set; }
}
