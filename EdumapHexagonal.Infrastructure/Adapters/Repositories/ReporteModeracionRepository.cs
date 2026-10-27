using EdumapHexagonal.Application.Common.Models;
using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Constants;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class ReporteModeracionRepository(ApplicationDbContext context) : IReporteModeracionRepository
{
    public Task<ReportesModeracion?> ObtenerPorIdAsync(int idReporte, CancellationToken ct = default)
    {
        return context.ReportesModeracions
            .Include(r => r.IdResenaNavigation)
            .FirstOrDefaultAsync(r => r.IdReporte == idReporte, ct);
    }

    public Task<ReportesModeracion?> ObtenerDetalleAsync(int idReporte, CancellationToken ct = default)
    {
        return context.ReportesModeracions
            .Include(r => r.IdResenaNavigation)
                .ThenInclude(res => res.IdUsuarioNavigation)
            .Include(r => r.IdResenaNavigation)
                .ThenInclude(res => res.IdInstitucionNavigation)
            .Include(r => r.IdUsuarioReportaNavigation)
            .Include(r => r.IdAdminRevisorNavigation)
            .FirstOrDefaultAsync(r => r.IdReporte == idReporte, ct);
    }

    public Task<bool> ExistePendienteAsync(int idResena, int idUsuarioReporta, CancellationToken ct = default)
    {
        return context.ReportesModeracions.AnyAsync(
            r => r.IdResena == idResena &&
                 r.IdUsuarioReporta == idUsuarioReporta &&
                 r.EstadoRevision == EstadosRevision.Pendiente, ct);
    }

    public async Task<IReadOnlyList<ReportesModeracion>> ListarPendientesPorResenaAsync(int idResena, CancellationToken ct = default)
    {
        return await context.ReportesModeracions
            .Where(r => r.IdResena == idResena && r.EstadoRevision == EstadosRevision.Pendiente)
            .ToListAsync(ct);
    }

    public async Task<PagedResult<ReportesModeracion>> ListarPendientesAsync(int pagina, int tamanoPagina, CancellationToken ct = default)
    {
        var query = context.ReportesModeracions
            .Include(r => r.IdResenaNavigation)
                .ThenInclude(res => res.IdInstitucionNavigation)
            .Include(r => r.IdUsuarioReportaNavigation)
            .Where(r => r.EstadoRevision == EstadosRevision.Pendiente)
            .OrderBy(r => r.FechaReporte);

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(ct);

        return new PagedResult<ReportesModeracion>(items, total, pagina, tamanoPagina);
    }

    public void Agregar(ReportesModeracion reporte)
    {
        context.ReportesModeracions.Add(reporte);
    }
}
