using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniqueDraw.Domain.Entities.Base;
using UniqueDraw.Domain.Ports.Persistence;

namespace UniqueDraw.Infrastructure.Adapters.Persistence.Repositories;

public class EfCoreRepository<T> : IRepository<T> where T : class, IEntityBase<Guid>
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public EfCoreRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
            query = query.Include(include);

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.Where(predicate);
        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
}
