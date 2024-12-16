using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Infrastructure.Adapters.Persistence.EFContext;

namespace UniqueDraw.Infrastructure.Adapters.Persistence.Repositories;

public class UnitOfWork(UniqueDrawDbContext dbContext) : IUnitOfWork
{
    public async Task<int> CommitAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public void Rollback()
    {
        dbContext.Dispose();
    }
}