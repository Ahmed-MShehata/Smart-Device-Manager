namespace SDM.Domain.Common;

/// <summary>
/// Base class for all domain entities.
/// Provides a Guid primary key and a creation timestamp.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>Gets the unique identifier for this entity.</summary>
    public Guid Id { get; protected set; }

    /// <summary>Gets the UTC date and time when this entity was created.</summary>
    public DateTime CreatedAt { get; protected set; }

    /// <summary>Initializes a new instance of <see cref="BaseEntity"/> with a new Guid and the current UTC time.</summary>
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
