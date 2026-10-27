namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.PublicarOferta;

public sealed class PublicarOfertaValidator : AbstractValidator<PublicarOfertaCommand>
{
    public PublicarOfertaValidator()
    {
        RuleFor(x => x.IdSede).GreaterThan(0);
        RuleFor(x => x.IdPrograma).GreaterThan(0);
        RuleFor(x => x.IdModalidad).GreaterThan((short)0);
        RuleFor(x => x.IdTurno).GreaterThan((short)0);
        RuleFor(x => x.AnioAcademico).InclusiveBetween((short)2000, (short)2100);

        RuleFor(x => x.CostoInscripcion).GreaterThanOrEqualTo(0).PrecisionScale(10, 2, true);
        RuleFor(x => x.CostoMatricula).GreaterThanOrEqualTo(0).PrecisionScale(10, 2, true);
        RuleFor(x => x.PensionMensual).GreaterThanOrEqualTo(0).PrecisionScale(10, 2, true);
        RuleFor(x => x.CuotasAnuales).InclusiveBetween(OfertasEducativa.CuotasMinimas, OfertasEducativa.CuotasMaximas);
    }
}
