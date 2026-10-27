namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.FiltrarOfertasPorCosto;

public sealed class FiltrarOfertasPorCostoValidator : AbstractValidator<FiltrarOfertasPorCostoQuery>
{
    public FiltrarOfertasPorCostoValidator()
    {
        RuleFor(x => x.PensionMinima).GreaterThanOrEqualTo(0).When(x => x.PensionMinima is not null);
        RuleFor(x => x.PensionMaxima).GreaterThanOrEqualTo(0).When(x => x.PensionMaxima is not null);

        RuleFor(x => x)
            .Must(x => x.PensionMinima <= x.PensionMaxima)
            .When(x => x.PensionMinima is not null && x.PensionMaxima is not null)
            .WithName("Rango de pensiÃ³n")
            .WithMessage("La pensiÃ³n mÃ­nima no puede ser mayor que la pensiÃ³n mÃ¡xima.");

        this.ReglasPaginacion();
    }
}
