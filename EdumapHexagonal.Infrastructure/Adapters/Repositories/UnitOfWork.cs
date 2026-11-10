using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var repository))
        {
            return (IGenericRepository<T>)repository;
        }

        var repositoryInstance = new GenericRepository<T>(_context);
        _repositories[typeof(T)] = repositoryInstance;
        return repositoryInstance;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<int> Complete(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

