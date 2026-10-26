namespace EdumapHexagonal.Application.UseCases.Auth.Commands.RegistrarUsuario;

public sealed class RegistrarUsuarioHandler(
    IUsuarioRepository usuarios,
    IRolRepository roles,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegistrarUsuarioCommand, int>
{
    public async Task<int> Handle(RegistrarUsuarioCommand request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (await usuarios.ExisteEmailAsync(email, ct))
            throw new ConflictException("Ya existe una cuenta registrada con ese email.");

        var rolPorDefecto = await roles.ObtenerPorNombreAsync(RolesSistema.PorDefectoAlRegistrar, ct)
            ?? throw new BusinessRuleException(
                $"No existe el rol '{RolesSistema.PorDefectoAlRegistrar}' en la base de datos. Revisa RolesSistema.");

        var usuario = new Usuario
        {
            IdRol = rolPorDefecto.IdRol,
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            Email = email,
            PasswordHash = passwordHasher.Hash(request.Password),
            Activo = true
        };

        usuarios.Agregar(usuario);
        await unitOfWork.SaveChangesAsync(ct);

        return usuario.IdUsuario;
    }
}
