namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IFavoritoRepository
{
    Task<FavoritosComparador?> ObtenerAsync(int idUsuario, int idInstitucion, CancellationToken ct = default);

    Task<int> ContarPorUsuarioAsync(int idUsuario, CancellationToken ct = default);

    Task<IReadOnlyList<FavoritosComparador>> ListarPorUsuarioAsync(int idUsuario, CancellationToken ct = default);

    void Agregar(FavoritosComparador favorito);

    void Eliminar(FavoritosComparador favorito);
}
