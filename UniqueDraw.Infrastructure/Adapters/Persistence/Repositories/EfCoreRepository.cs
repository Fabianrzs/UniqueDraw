using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniqueDraw.Domain.Entities.Base;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Infrastructure.Adapters.Persistence.EFContext;

namespace UniqueDraw.Infrastructure.Adapters.Persistence.Repositories;

public class EFCoreRepository<T>(UniqueDrawDbContext context) : IRepository<T> where T : EntityBase 
{ 
    private readonly DbSet<T> dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach (var include in includes)
            query = query.Include(include);

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet.Where(predicate);
        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbSet.AnyAsync(predicate);
    }
}
