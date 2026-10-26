namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IUsuarioRepository
{
    /// <summary>Incluye el rol (IdRolNavigation).</summary>
    Task<Usuario?> ObtenerPorIdAsync(int idUsuario, CancellationToken ct = default);

    /// <summary>Busca sin distinguir mayúsculas. Incluye el rol (IdRolNavigation).</summary>
    Task<Usuario?> ObtenerPorEmailAsync(string email, CancellationToken ct = default);

    Task<bool> ExisteEmailAsync(string email, CancellationToken ct = default);

    /// <summary>Incluye el rol. Busqueda filtra por nombres, apellidos o email.</summary>
    Task<PagedResult<Usuario>> ListarAsync(FiltroUsuarios filtro, CancellationToken ct = default);

    void Agregar(Usuario usuario);
}
