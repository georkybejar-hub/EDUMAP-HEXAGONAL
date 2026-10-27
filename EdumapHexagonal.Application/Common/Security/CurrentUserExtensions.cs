namespace EdumapHexagonal.Application.Common.Security;

public static class CurrentUserExtensions
{
    public static int ObtenerIdRequerido(this ICurrentUserService usuario) =>
        usuario.IdUsuario ?? throw new UnauthorizedException("Debes iniciar sesiÃ³n para realizar esta acciÃ³n.");

    public static bool TieneRol(this ICurrentUserService usuario, string rol) =>
        string.Equals(usuario.Rol, rol, StringComparison.OrdinalIgnoreCase);

    public static bool EsAdministrador(this ICurrentUserService usuario) =>
        usuario.TieneRol(RolesSistema.Administrador);

    public static int RequerirAdministrador(this ICurrentUserService usuario)
    {
        var idUsuario = usuario.ObtenerIdRequerido();
        if (!usuario.EsAdministrador())
            throw new ForbiddenException("Esta acciÃ³n solo estÃ¡ permitida para administradores.");
        return idUsuario;
    }

    public static int ValidarGestionInstitucion(this ICurrentUserService usuario, Institucione institucion)
    {
        var idUsuario = usuario.ObtenerIdRequerido();
        if (usuario.EsAdministrador() || institucion.EsGestionadaPor(idUsuario))
            return idUsuario;

        throw new ForbiddenException("No tienes permisos para gestionar esta instituciÃ³n.");
    }
}
