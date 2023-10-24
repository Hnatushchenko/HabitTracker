using Domain;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationContext _applicationContext;

    public UnitOfWork(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}