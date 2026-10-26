namespace EdumapHexagonal.Application.UseCases.Usuarios.Queries.ListarUsuarios;

public sealed class ListarUsuariosHandler(
    IUsuarioRepository usuarios,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ListarUsuariosQuery, PagedResult<UsuarioListadoDto>>
{
    public async Task<PagedResult<UsuarioListadoDto>> Handle(ListarUsuariosQuery request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var filtro = new FiltroUsuarios(
            request.Busqueda?.Trim(),
            request.IdRol,
            request.Activo,
            request.Pagina,
            request.TamanoPagina);

        var pagina = await usuarios.ListarAsync(filtro, ct);

        return pagina.Map(u => new UsuarioListadoDto(
            u.IdUsuario,
            $"{u.Nombres} {u.Apellidos}",
            u.Email,
            u.IdRolNavigation.NombreRol,
            u.Activo != false,
            u.FechaRegistro));
    }
}
