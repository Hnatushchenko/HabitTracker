using OneOf.Types;

namespace Domain;

public interface IRepository<TEntity, in TEntityId>
{
    Task<List<TEntity>> GetAllAsync();
    Task<OneOf<TEntity, NotFound>> GetByIdDeprecatedAsync(TEntityId entityId);
    Task<TEntity> GetByIdAsync(TEntityId entityId, CancellationToken cancellationToken);
    void Add(TEntity entity);
    void Remove(TEntity entity);
}