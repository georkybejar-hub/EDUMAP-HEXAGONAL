namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IResenaRepository
{
    Task<Resena?> ObtenerPorIdAsync(int idResena, CancellationToken ct = default);

    Task<bool> ExisteDeUsuarioAsync(int idInstitucion, int idUsuario, CancellationToken ct = default);

    Task<PagedResult<Resena>> ListarVisiblesPorInstitucionAsync(
        int idInstitucion, int pagina, int tamanoPagina, CancellationToken ct = default);

    Task<IReadOnlyDictionary<short, int>> ObtenerDistribucionPuntuacionAsync(int idInstitucion, CancellationToken ct = default);

    Task<IReadOnlyList<Resena>> ListarPorUsuarioAsync(int idUsuario, CancellationToken ct = default);

    void Agregar(Resena resena);

    void Eliminar(Resena resena);
}
