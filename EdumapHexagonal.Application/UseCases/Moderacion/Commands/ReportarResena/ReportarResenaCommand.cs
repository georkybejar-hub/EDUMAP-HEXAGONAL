namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ReportarResena;

/// <summary>
/// Reporta una reseña inapropiada. No se puede reportar la propia reseña
/// ni reportar dos veces la misma reseña mientras el primer reporte esté pendiente.
/// </summary>
/// <returns>Id del reporte creado.</returns>
public sealed record ReportarResenaCommand(int IdResena, string MotivoReporte) : IRequest<int>;
