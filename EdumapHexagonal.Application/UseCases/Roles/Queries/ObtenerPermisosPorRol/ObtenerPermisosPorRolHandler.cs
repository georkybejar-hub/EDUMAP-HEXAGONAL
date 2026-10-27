namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ObtenerPermisosPorRol;

public sealed class ObtenerPermisosPorRolHandler(
    IRolRepository roles,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ObtenerPermisosPorRolQuery, PermisosRolDto>
{
    public async Task<PermisosRolDto> Handle(ObtenerPermisosPorRolQuery request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var rol = await roles.ObtenerPorIdAsync(request.IdRol, ct)
            ?? throw new NotFoundException("Rol", request.IdRol);

        var permisos = await roles.ListarPermisosPorRolAsync(request.IdRol, ct);

        return new PermisosRolDto(
            rol.IdRol,
            rol.NombreRol,
            permisos.Select(p => new PermisoDto(p.IdPermiso, p.CodigoPermiso, p.ModuloSistema)).ToList());
    }
}
