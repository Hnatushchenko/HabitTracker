using OneOf.Types;

namespace Domain;

public interface IRepository<TEntity, in TId>
{
    Task<List<TEntity>> GetAllAsync();
    Task<OneOf<TEntity, NotFound>> GetByIdDeprecatedAsync(TId entityId);
    void Add(TEntity entity);
    void Remove(TEntity entity);
}