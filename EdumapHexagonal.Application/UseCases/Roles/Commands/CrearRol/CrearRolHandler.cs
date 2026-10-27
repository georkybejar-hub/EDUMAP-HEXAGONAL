namespace EdumapHexagonal.Application.UseCases.Roles.Commands.CrearRol;

public sealed class CrearRolHandler(
    IRolRepository roles,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CrearRolCommand, short>
{
    public async Task<short> Handle(CrearRolCommand request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var nombre = request.NombreRol.Trim().ToUpperInvariant();

        if (await roles.ExisteNombreAsync(nombre, ct))
            throw new ConflictException($"Ya existe un rol llamado '{nombre}'.");

        var rol = new Role
        {
            NombreRol = nombre,
            DescripcionRol = request.DescripcionRol.Trim()
        };

        roles.Agregar(rol);
        await unitOfWork.SaveChangesAsync(ct);

        return rol.IdRol;
    }
}
