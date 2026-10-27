namespace EdumapHexagonal.Application.UseCases.Roles.Commands.CrearRol;

public sealed class CrearRolValidator : AbstractValidator<CrearRolCommand>
{
    public CrearRolValidator()
    {
        RuleFor(x => x.NombreRol)
            .NotEmpty()
            .MaximumLength(50)
            .Matches("^[A-Za-z_ ]+$").WithMessage("El nombre del rol solo puede contener letras, espacios y guiones bajos.");

        RuleFor(x => x.DescripcionRol).NotEmpty().MaximumLength(200);
    }
}
