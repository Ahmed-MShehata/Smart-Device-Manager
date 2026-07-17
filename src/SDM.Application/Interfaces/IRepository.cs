using SDM.Domain.Common;

namespace SDM.Application.Interfaces;

/// <summary>
/// Generic repository interface providing basic data access operations for any entity.
/// </summary>
/// <typeparam name="T">Entity type that inherits from <see cref="BaseEntity"/>.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>Returns the entity with the given identifier, or null if not found.</summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Returns all entities of type <typeparamref name="T"/>.</summary>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Adds a new entity to the context (not yet saved).</summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>Marks an existing entity as modified (not yet saved).</summary>
    void Update(T entity);

    /// <summary>Marks an entity for deletion (not yet saved).</summary>
    void Remove(T entity);

    /// <summary>Returns true if any entity with the given identifier exists.</summary>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
