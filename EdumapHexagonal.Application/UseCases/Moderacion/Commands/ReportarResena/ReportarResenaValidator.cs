namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ReportarResena;

public sealed class ReportarResenaValidator : AbstractValidator<ReportarResenaCommand>
{
    public ReportarResenaValidator()
    {
        RuleFor(x => x.IdResena).GreaterThan(0);
        RuleFor(x => x.MotivoReporte)
            .NotEmpty()
            .Length(ReglasNegocio.MotivoReporteLongitudMinima, 150);
    }
}
