namespace Domain;

public interface IRepository<TEntity, in TEntityId>
{
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(TEntityId entityId, CancellationToken cancellationToken);
    void Add(TEntity entity);
    void Remove(TEntity entity);
}