namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActualizarPerfil;

public sealed class ActualizarPerfilHandler(
    IUsuarioRepository usuarios,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ActualizarPerfilCommand>
{
    public async Task Handle(ActualizarPerfilCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var usuario = await usuarios.ObtenerPorIdAsync(idUsuario, ct)
            ?? throw new NotFoundException("Usuario", idUsuario);

        usuario.Nombres = request.Nombres.Trim();
        usuario.Apellidos = request.Apellidos.Trim();

        await unitOfWork.SaveChangesAsync(ct);
    }
}
