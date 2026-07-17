using Microsoft.EntityFrameworkCore;
using SDM.Application.Interfaces;
using SDM.Domain.Common;
using SDM.Infrastructure.Persistence;

namespace SDM.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation backed by <see cref="ApplicationDbContext"/>.
/// Provides basic CRUD operations for any entity that inherits <see cref="BaseEntity"/>.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
internal sealed class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    /// <summary>Initializes a new instance of <see cref="Repository{T}"/>.</summary>
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <inheritdoc/>
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);

    /// <inheritdoc/>
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    /// <inheritdoc/>
    public void Update(T entity)
        => _dbSet.Update(entity);

    /// <inheritdoc/>
    public void Remove(T entity)
        => _dbSet.Remove(entity);

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
}
