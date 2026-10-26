namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ObtenerSedesCercanas;

public sealed class ObtenerSedesCercanasValidator : AbstractValidator<ObtenerSedesCercanasQuery>
{
    public ObtenerSedesCercanasValidator()
    {
        RuleFor(x => x.Latitud).LatitudPeru();
        RuleFor(x => x.Longitud).LongitudPeru();

        RuleFor(x => x.RadioKm)
            .GreaterThan(0)
            .LessThanOrEqualTo(ReglasNegocio.RadioBusquedaMaximoKm)
            .WithMessage($"El radio debe ser mayor que 0 y como máximo {ReglasNegocio.RadioBusquedaMaximoKm} km.");

        RuleFor(x => x.Maximo).InclusiveBetween(1, ReglasNegocio.MaximoSedesCercanas);
    }
}
