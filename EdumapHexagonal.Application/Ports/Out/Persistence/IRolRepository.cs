namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IRolRepository
{
    Task<Role?> ObtenerPorIdAsync(short idRol, CancellationToken ct = default);

    /// <summary>Busca sin distinguir mayúsculas.</summary>
    Task<Role?> ObtenerPorNombreAsync(string nombreRol, CancellationToken ct = default);

    Task<bool> ExisteNombreAsync(string nombreRol, CancellationToken ct = default);

    Task<IReadOnlyList<Role>> ListarAsync(CancellationToken ct = default);

    Task<Permiso?> ObtenerPermisoPorIdAsync(short idPermiso, CancellationToken ct = default);

    /// <summary>Ordenados por módulo y código. Si se indica módulo, filtra por él.</summary>
    Task<IReadOnlyList<Permiso>> ListarPermisosAsync(string? moduloSistema, CancellationToken ct = default);

    Task<IReadOnlyList<Permiso>> ListarPermisosPorRolAsync(short idRol, CancellationToken ct = default);

    Task<IReadOnlyList<string>> ObtenerCodigosPermisoAsync(short idRol, CancellationToken ct = default);

    Task<RolPermiso?> ObtenerRolPermisoAsync(short idRol, short idPermiso, CancellationToken ct = default);

    void Agregar(Role rol);

    void AgregarRolPermiso(RolPermiso rolPermiso);

    void EliminarRolPermiso(RolPermiso rolPermiso);
}
