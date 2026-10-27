namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ListarReportesPendientes;

public sealed class ListarReportesPendientesHandler(
    IReporteModeracionRepository reportes,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ListarReportesPendientesQuery, PagedResult<ReportePendienteDto>>
{
    public async Task<PagedResult<ReportePendienteDto>> Handle(ListarReportesPendientesQuery request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var pagina = await reportes.ListarPendientesAsync(request.Pagina, request.TamanoPagina, ct);

        return pagina.Map(r => new ReportePendienteDto(
            r.IdReporte,
            r.IdResena,
            r.IdResenaNavigation.Comentario,
            r.IdResenaNavigation.Puntuacion,
            r.IdResenaNavigation.IdInstitucion,
            r.IdResenaNavigation.IdInstitucionNavigation.Nombre,
            $"{r.IdUsuarioReportaNavigation.Nombres} {r.IdUsuarioReportaNavigation.Apellidos}",
            r.MotivoReporte,
            r.FechaReporte));
    }
}
