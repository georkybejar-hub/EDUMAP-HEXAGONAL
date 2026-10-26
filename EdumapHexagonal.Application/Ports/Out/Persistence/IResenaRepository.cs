namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IResenaRepository
{
    Task<Resena?> ObtenerPorIdAsync(int idResena, CancellationToken ct = default);

    Task<bool> ExisteDeUsuarioAsync(int idInstitucion, int idUsuario, CancellationToken ct = default);

    /// <summary>Solo visibles, más recientes primero. Incluye el usuario autor.</summary>
    Task<PagedResult<Resena>> ListarVisiblesPorInstitucionAsync(
        int idInstitucion, int pagina, int tamanoPagina, CancellationToken ct = default);

    /// <summary>Cantidad de reseñas visibles por puntuación (1..5).</summary>
    Task<IReadOnlyDictionary<short, int>> ObtenerDistribucionPuntuacionAsync(int idInstitucion, CancellationToken ct = default);

    /// <summary>Más recientes primero. Incluye la institución.</summary>
    Task<IReadOnlyList<Resena>> ListarPorUsuarioAsync(int idUsuario, CancellationToken ct = default);

    void Agregar(Resena resena);

    void Eliminar(Resena resena);
}
