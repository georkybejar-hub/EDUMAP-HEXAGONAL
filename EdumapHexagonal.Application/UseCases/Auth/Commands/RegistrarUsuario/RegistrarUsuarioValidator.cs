namespace EdumapHexagonal.Application.UseCases.Auth.Commands.RegistrarUsuario;

public sealed class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioCommand>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(x => x.Nombres).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Apellidos).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(150);
        RuleFor(x => x.Password).PasswordSegura();
    }
}
