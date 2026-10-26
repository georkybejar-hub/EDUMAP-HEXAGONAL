namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.CambiarPassword;

public sealed class CambiarPasswordValidator : AbstractValidator<CambiarPasswordCommand>
{
    public CambiarPasswordValidator()
    {
        RuleFor(x => x.PasswordActual).NotEmpty();

        RuleFor(x => x.PasswordNueva)
            .PasswordSegura()
            .NotEqual(x => x.PasswordActual).WithMessage("La nueva contraseña debe ser distinta de la actual.");
    }
}
