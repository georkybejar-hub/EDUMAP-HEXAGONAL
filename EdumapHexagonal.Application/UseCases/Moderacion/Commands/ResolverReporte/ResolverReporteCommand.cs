namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ResolverReporte;

public enum DecisionModeracion
{
    /// <summary>El reporte procede: se oculta la reseña y se cierran los demás reportes pendientes sobre ella.</summary>
    OcultarResena,

    /// <summary>El reporte no procede: la reseña sigue visible.</summary>
    Descartar
}

/// <summary>Solo administradores.</summary>
public sealed record ResolverReporteCommand(
    int IdReporte,
    DecisionModeracion Decision,
    string ResolucionAdmin) : IRequest;
