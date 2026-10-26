namespace EdumapHexagonal.Application.UseCases.Usuarios.Queries.ObtenerPerfil;

public sealed class ObtenerPerfilHandler(
    IUsuarioRepository usuarios,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ObtenerPerfilQuery, PerfilDto>
{
    public async Task<PerfilDto> Handle(ObtenerPerfilQuery request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var usuario = await usuarios.ObtenerPorIdAsync(idUsuario, ct)
            ?? throw new NotFoundException("Usuario", idUsuario);

        return new PerfilDto(
            usuario.IdUsuario,
            usuario.Nombres,
            usuario.Apellidos,
            usuario.Email,
            usuario.IdRolNavigation.NombreRol,
            usuario.Activo != false,
            usuario.FechaRegistro);
    }
}
