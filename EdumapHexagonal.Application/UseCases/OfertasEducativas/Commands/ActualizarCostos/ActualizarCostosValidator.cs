namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.ActualizarCostos;

public sealed class ActualizarCostosValidator : AbstractValidator<ActualizarCostosCommand>
{
    public ActualizarCostosValidator()
    {
        RuleFor(x => x.IdOferta).GreaterThan(0);
        RuleFor(x => x.CostoInscripcion).GreaterThanOrEqualTo(0).PrecisionScale(10, 2, true);
        RuleFor(x => x.CostoMatricula).GreaterThanOrEqualTo(0).PrecisionScale(10, 2, true);
        RuleFor(x => x.PensionMensual).GreaterThanOrEqualTo(0).PrecisionScale(10, 2, true);
        RuleFor(x => x.CuotasAnuales).InclusiveBetween(OfertasEducativa.CuotasMinimas, OfertasEducativa.CuotasMaximas);
    }
}
