namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.BuscarInstituciones;

public sealed class BuscarInstitucionesValidator : AbstractValidator<BuscarInstitucionesQuery>
{
    public BuscarInstitucionesValidator()
    {
        RuleFor(x => x.Texto).MaximumLength(100);
        RuleFor(x => x.PensionMaxima).GreaterThanOrEqualTo(0).When(x => x.PensionMaxima is not null);
        RuleFor(x => x.CalificacionMinima)
            .InclusiveBetween(0, Resena.PuntuacionMaxima)
            .When(x => x.CalificacionMinima is not null);
        RuleFor(x => x.Orden).IsInEnum();
        this.ReglasPaginacion();
    }
}
