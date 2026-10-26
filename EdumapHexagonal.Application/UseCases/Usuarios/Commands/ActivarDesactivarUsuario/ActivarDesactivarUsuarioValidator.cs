namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActivarDesactivarUsuario;

public sealed class ActivarDesactivarUsuarioValidator : AbstractValidator<ActivarDesactivarUsuarioCommand>
{
    public ActivarDesactivarUsuarioValidator()
    {
        RuleFor(x => x.IdUsuario).GreaterThan(0);
    }
}
