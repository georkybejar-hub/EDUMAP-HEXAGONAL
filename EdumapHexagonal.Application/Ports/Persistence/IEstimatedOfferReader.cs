namespace EdumapHexagonal.Application.Ports.Persistence;

/// <summary>Consulta la marca opcional creada por el SQL 04, sin exigir su instalación.</summary>
public interface IEstimatedOfferReader
{
    Task<IReadOnlySet<int>> GetEstimatedOfferIdsAsync(
        IReadOnlyCollection<int> offerIds, CancellationToken cancellationToken = default);
}
