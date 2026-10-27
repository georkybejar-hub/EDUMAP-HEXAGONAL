namespace EdumapHexagonal.Application.UseCases.Auth.Commands.Login;

public sealed class LoginHandler(
    IUsuarioRepository usuarios,
    IRolRepository roles,
    IPasswordHasher passwordHasher,
    ITokenService tokenService)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var usuario = await usuarios.ObtenerPorEmailAsync(email, ct);

        if (usuario is null || !passwordHasher.Verificar(request.Password, usuario.PasswordHash))
            throw new UnauthorizedException("Email o contraseÃ±a incorrectos.");

        if (usuario.Activo == false)
            throw new ForbiddenException("Tu cuenta estÃ¡ desactivada. ComunÃ­cate con un administrador.");

        var rol = usuario.IdRolNavigation.NombreRol;
        var permisos = await roles.ObtenerCodigosPermisoAsync(usuario.IdRol, ct);
        var token = tokenService.GenerarToken(usuario, rol, permisos);

        return new LoginResponse(
            usuario.IdUsuario,
            $"{usuario.Nombres} {usuario.Apellidos}",
            usuario.Email,
            rol,
            permisos,
            token.Token,
            token.Expiracion);
    }
}
