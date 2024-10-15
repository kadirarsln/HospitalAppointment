using Core.Entities;

namespace Core.Repositories;

public interface IRepository<TEntity, TId>
    where TEntity : Entity<TId>, new()
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<TEntity?> AddAsync(TEntity entity);
    Task<TEntity?> RemoveAsync(TEntity entity);
}
