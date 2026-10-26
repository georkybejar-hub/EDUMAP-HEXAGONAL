namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActivarDesactivarUsuario;

public sealed class ActivarDesactivarUsuarioHandler(
    IUsuarioRepository usuarios,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ActivarDesactivarUsuarioCommand>
{
    public async Task Handle(ActivarDesactivarUsuarioCommand request, CancellationToken ct)
    {
        var idAdmin = usuarioActual.RequerirAdministrador();

        if (request.IdUsuario == idAdmin && !request.Activo)
            throw new BusinessRuleException("No puedes desactivar tu propia cuenta.");

        var usuario = await usuarios.ObtenerPorIdAsync(request.IdUsuario, ct)
            ?? throw new NotFoundException("Usuario", request.IdUsuario);

        usuario.Activo = request.Activo;

        await unitOfWork.SaveChangesAsync(ct);
    }
}
