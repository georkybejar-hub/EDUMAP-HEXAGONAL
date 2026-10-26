namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.RegistrarSede;

public sealed class RegistrarSedeValidator : AbstractValidator<RegistrarSedeCommand>
{
    public RegistrarSedeValidator()
    {
        RuleFor(x => x.IdInstitucion).GreaterThan(0);
        RuleFor(x => x.IdDistrito).GreaterThan((short)0);
        RuleFor(x => x.NombreSede).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Direccion).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Referencia).MaximumLength(200);
        RuleFor(x => x.Latitud).LatitudPeru();
        RuleFor(x => x.Longitud).LongitudPeru();
    }
}
