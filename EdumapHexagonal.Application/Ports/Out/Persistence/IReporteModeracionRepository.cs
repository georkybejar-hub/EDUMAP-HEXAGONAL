namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IReporteModeracionRepository
{
    Task<ReportesModeracion?> ObtenerPorIdAsync(int idReporte, CancellationToken ct = default);

    Task<ReportesModeracion?> ObtenerDetalleAsync(int idReporte, CancellationToken ct = default);

    Task<bool> ExistePendienteAsync(int idResena, int idUsuarioReporta, CancellationToken ct = default);

    Task<IReadOnlyList<ReportesModeracion>> ListarPendientesPorResenaAsync(int idResena, CancellationToken ct = default);

    Task<PagedResult<ReportesModeracion>> ListarPendientesAsync(int pagina, int tamanoPagina, CancellationToken ct = default);

    void Agregar(ReportesModeracion reporte);
}
