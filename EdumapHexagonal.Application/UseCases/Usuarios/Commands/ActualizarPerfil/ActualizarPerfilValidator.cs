namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActualizarPerfil;

public sealed class ActualizarPerfilValidator : AbstractValidator<ActualizarPerfilCommand>
{
    public ActualizarPerfilValidator()
    {
        RuleFor(x => x.Nombres).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Apellidos).NotEmpty().MaximumLength(100);
    }
}
