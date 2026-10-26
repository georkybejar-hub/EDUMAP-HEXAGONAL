namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.CambiarPassword;

public sealed class CambiarPasswordHandler(
    IUsuarioRepository usuarios,
    IPasswordHasher passwordHasher,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CambiarPasswordCommand>
{
    public async Task Handle(CambiarPasswordCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var usuario = await usuarios.ObtenerPorIdAsync(idUsuario, ct)
            ?? throw new NotFoundException("Usuario", idUsuario);

        if (!passwordHasher.Verificar(request.PasswordActual, usuario.PasswordHash))
            throw new BusinessRuleException("La contraseña actual es incorrecta.");

        usuario.PasswordHash = passwordHasher.Hash(request.PasswordNueva);

        await unitOfWork.SaveChangesAsync(ct);
    }
}
