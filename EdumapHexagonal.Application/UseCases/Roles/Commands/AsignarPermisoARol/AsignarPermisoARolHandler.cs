namespace EdumapHexagonal.Application.UseCases.Roles.Commands.AsignarPermisoARol;

public sealed class AsignarPermisoARolHandler(
    IRolRepository roles,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AsignarPermisoARolCommand>
{
    public async Task Handle(AsignarPermisoARolCommand request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var rol = await roles.ObtenerPorIdAsync(request.IdRol, ct)
            ?? throw new NotFoundException("Rol", request.IdRol);

        var permiso = await roles.ObtenerPermisoPorIdAsync(request.IdPermiso, ct)
            ?? throw new NotFoundException("Permiso", request.IdPermiso);

        if (await roles.ObtenerRolPermisoAsync(request.IdRol, request.IdPermiso, ct) is not null)
            throw new ConflictException($"El rol '{rol.NombreRol}' ya tiene el permiso '{permiso.CodigoPermiso}'.");

        roles.AgregarRolPermiso(new RolPermiso
        {
            IdRol = request.IdRol,
            IdPermiso = request.IdPermiso
        });

        await unitOfWork.SaveChangesAsync(ct);
    }
}
