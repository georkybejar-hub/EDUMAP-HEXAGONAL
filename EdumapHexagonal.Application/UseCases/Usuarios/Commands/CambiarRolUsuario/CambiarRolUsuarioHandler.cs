namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.CambiarRolUsuario;

public sealed class CambiarRolUsuarioHandler(
    IUsuarioRepository usuarios,
    IRolRepository roles,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CambiarRolUsuarioCommand>
{
    public async Task Handle(CambiarRolUsuarioCommand request, CancellationToken ct)
    {
        var idAdmin = usuarioActual.RequerirAdministrador();

        if (request.IdUsuario == idAdmin)
            throw new BusinessRuleException("No puedes cambiar tu propio rol.");

        var usuario = await usuarios.ObtenerPorIdAsync(request.IdUsuario, ct)
            ?? throw new NotFoundException("Usuario", request.IdUsuario);

        if (usuario.IdRol == request.IdRol)
            return;

        _ = await roles.ObtenerPorIdAsync(request.IdRol, ct)
            ?? throw new NotFoundException("Rol", request.IdRol);

        usuario.IdRol = request.IdRol;

        await unitOfWork.SaveChangesAsync(ct);
    }
}
