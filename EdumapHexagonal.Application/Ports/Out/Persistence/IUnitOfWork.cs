namespace EdumapHexagonal.Application.Ports.Out.Persistence;

/// <summary>
/// Confirma en una sola transacción todos los cambios hechos a través de los repositorios.
/// Los repositorios solo registran cambios (Agregar/Eliminar/modificar entidades); este puerto los guarda.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
