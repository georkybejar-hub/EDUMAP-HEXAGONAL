namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ResolverReporte;

public sealed class ResolverReporteValidator : AbstractValidator<ResolverReporteCommand>
{
    public ResolverReporteValidator()
    {
        RuleFor(x => x.IdReporte).GreaterThan(0);
        RuleFor(x => x.Decision).IsInEnum();
        RuleFor(x => x.ResolucionAdmin).NotEmpty().MaximumLength(500);
    }
}
