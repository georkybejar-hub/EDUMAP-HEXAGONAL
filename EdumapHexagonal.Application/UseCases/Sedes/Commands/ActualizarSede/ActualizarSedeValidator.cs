namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.ActualizarSede;

public sealed class ActualizarSedeValidator : AbstractValidator<ActualizarSedeCommand>
{
    public ActualizarSedeValidator()
    {
        RuleFor(x => x.IdSede).GreaterThan(0);
        RuleFor(x => x.IdDistrito).GreaterThan((short)0);
        RuleFor(x => x.NombreSede).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Direccion).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Referencia).MaximumLength(200);
        RuleFor(x => x.Latitud).LatitudPeru();
        RuleFor(x => x.Longitud).LongitudPeru();
    }
}
