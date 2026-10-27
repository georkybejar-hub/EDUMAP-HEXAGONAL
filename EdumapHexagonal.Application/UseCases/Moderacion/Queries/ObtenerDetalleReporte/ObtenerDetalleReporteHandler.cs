namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ObtenerDetalleReporte;

public sealed class ObtenerDetalleReporteHandler(
    IReporteModeracionRepository reportes,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ObtenerDetalleReporteQuery, ReporteDetalleDto>
{
    public async Task<ReporteDetalleDto> Handle(ObtenerDetalleReporteQuery request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var r = await reportes.ObtenerDetalleAsync(request.IdReporte, ct)
            ?? throw new NotFoundException("Reporte", request.IdReporte);

        var pendientesDeLaResena = await reportes.ListarPendientesPorResenaAsync(r.IdResena, ct);
        var resena = r.IdResenaNavigation;

        return new ReporteDetalleDto(
            r.IdReporte,
            r.EstadoRevision,
            r.MotivoReporte,
            r.FechaReporte,
            NombreCompleto(r.IdUsuarioReportaNavigation),
            r.IdResena,
            resena.Comentario,
            resena.Puntuacion,
            resena.EstaVisible(),
            NombreCompleto(resena.IdUsuarioNavigation),
            resena.IdInstitucion,
            resena.IdInstitucionNavigation.Nombre,
            r.IdAdminRevisorNavigation is null ? null : NombreCompleto(r.IdAdminRevisorNavigation),
            r.ResolucionAdmin,
            r.FechaResolucion,
            pendientesDeLaResena.Count(p => p.IdReporte != r.IdReporte));
    }

    private static string NombreCompleto(Usuario u) => $"{u.Nombres} {u.Apellidos}";
}
