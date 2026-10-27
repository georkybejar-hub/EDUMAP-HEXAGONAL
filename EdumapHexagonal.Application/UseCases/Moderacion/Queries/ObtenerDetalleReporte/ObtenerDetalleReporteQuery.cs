namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ObtenerDetalleReporte;

public sealed record ObtenerDetalleReporteQuery(int IdReporte) : IRequest<ReporteDetalleDto>;
