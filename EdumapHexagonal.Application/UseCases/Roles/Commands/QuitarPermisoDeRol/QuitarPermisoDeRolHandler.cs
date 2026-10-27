namespace EdumapHexagonal.Application.UseCases.Roles.Commands.QuitarPermisoDeRol;

public sealed class QuitarPermisoDeRolHandler(
    IRolRepository roles,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<QuitarPermisoDeRolCommand>
{
    public async Task Handle(QuitarPermisoDeRolCommand request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var rol = await roles.ObtenerPorIdAsync(request.IdRol, ct)
            ?? throw new NotFoundException("Rol", request.IdRol);

        if (string.Equals(rol.NombreRol, RolesSistema.Administrador, StringComparison.OrdinalIgnoreCase))
            throw new BusinessRuleException("No se pueden quitar permisos al rol administrador.");

        var rolPermiso = await roles.ObtenerRolPermisoAsync(request.IdRol, request.IdPermiso, ct)
            ?? throw new NotFoundException($"El rol '{rol.NombreRol}' no tiene asignado el permiso {request.IdPermiso}.");

        roles.EliminarRolPermiso(rolPermiso);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
