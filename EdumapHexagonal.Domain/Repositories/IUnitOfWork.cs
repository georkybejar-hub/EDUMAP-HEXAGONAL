namespace EdumapHexagonal.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> Complete(CancellationToken cancellationToken = default);
}

