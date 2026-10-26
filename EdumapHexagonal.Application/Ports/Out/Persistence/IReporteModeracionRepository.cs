namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IReporteModeracionRepository
{
    /// <summary>Incluye la reseña reportada.</summary>
    Task<ReportesModeracion?> ObtenerPorIdAsync(int idReporte, CancellationToken ct = default);

    /// <summary>Incluye reseña (autor e institución), usuario que reporta y administrador revisor.</summary>
    Task<ReportesModeracion?> ObtenerDetalleAsync(int idReporte, CancellationToken ct = default);

    Task<bool> ExistePendienteAsync(int idResena, int idUsuarioReporta, CancellationToken ct = default);

    Task<IReadOnlyList<ReportesModeracion>> ListarPendientesPorResenaAsync(int idResena, CancellationToken ct = default);

    /// <summary>Más antiguos primero. Incluye reseña (con institución) y usuario que reporta.</summary>
    Task<PagedResult<ReportesModeracion>> ListarPendientesAsync(int pagina, int tamanoPagina, CancellationToken ct = default);

    void Agregar(ReportesModeracion reporte);
}
