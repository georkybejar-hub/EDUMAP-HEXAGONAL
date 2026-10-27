namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorIdAsync(int idUsuario, CancellationToken ct = default);

    Task<Usuario?> ObtenerPorEmailAsync(string email, CancellationToken ct = default);

    Task<bool> ExisteEmailAsync(string email, CancellationToken ct = default);

    Task<PagedResult<Usuario>> ListarAsync(FiltroUsuarios filtro, CancellationToken ct = default);

    void Agregar(Usuario usuario);
}
