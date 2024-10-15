using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

public class EfRepositoryBase<TContext, TEntity, TId> : IRepository<TEntity, TId>
where TEntity : Entity<TId>, new()
where TContext : DbContext
{
    protected TContext Context;

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(Context.Set<TEntity>());
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        entity.UpdatedDate = DateTime.Now;
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        entity.CreatedDate = DateTime.Now;
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity?> RemoveAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
}

