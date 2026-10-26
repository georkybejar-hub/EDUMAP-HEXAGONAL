namespace EdumapHexagonal.Application.Common.Security;

/// <summary>Comprobaciones de autorización reutilizables por los handlers.</summary>
public static class CurrentUserExtensions
{
    public static int ObtenerIdRequerido(this ICurrentUserService usuario) =>
        usuario.IdUsuario ?? throw new UnauthorizedException("Debes iniciar sesión para realizar esta acción.");

    public static bool TieneRol(this ICurrentUserService usuario, string rol) =>
        string.Equals(usuario.Rol, rol, StringComparison.OrdinalIgnoreCase);

    public static bool EsAdministrador(this ICurrentUserService usuario) =>
        usuario.TieneRol(RolesSistema.Administrador);

    /// <returns>El id del administrador autenticado.</returns>
    public static int RequerirAdministrador(this ICurrentUserService usuario)
    {
        var idUsuario = usuario.ObtenerIdRequerido();
        if (!usuario.EsAdministrador())
            throw new ForbiddenException("Esta acción solo está permitida para administradores.");
        return idUsuario;
    }

    /// <summary>Solo el representante de la institución o un administrador pueden modificarla.</summary>
    /// <returns>El id del usuario autenticado.</returns>
    public static int ValidarGestionInstitucion(this ICurrentUserService usuario, Institucione institucion)
    {
        var idUsuario = usuario.ObtenerIdRequerido();
        if (usuario.EsAdministrador() || institucion.EsGestionadaPor(idUsuario))
            return idUsuario;

        throw new ForbiddenException("No tienes permisos para gestionar esta institución.");
    }
}
