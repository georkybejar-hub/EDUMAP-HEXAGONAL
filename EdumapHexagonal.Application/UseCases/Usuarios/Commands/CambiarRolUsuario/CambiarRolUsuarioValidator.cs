namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.CambiarRolUsuario;

public sealed class CambiarRolUsuarioValidator : AbstractValidator<CambiarRolUsuarioCommand>
{
    public CambiarRolUsuarioValidator()
    {
        RuleFor(x => x.IdUsuario).GreaterThan(0);
        RuleFor(x => x.IdRol).GreaterThan((short)0);
    }
}
