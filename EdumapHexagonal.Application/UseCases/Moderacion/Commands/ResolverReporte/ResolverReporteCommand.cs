namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ResolverReporte;

public enum DecisionModeracion
{
    OcultarResena,

    Descartar
}

public sealed record ResolverReporteCommand(
    int IdReporte,
    DecisionModeracion Decision,
    string ResolucionAdmin) : IRequest;
