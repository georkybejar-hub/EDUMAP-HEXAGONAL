namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ReportarResena;

public sealed class ReportarResenaHandler(
    IResenaRepository resenas,
    IReporteModeracionRepository reportes,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ReportarResenaCommand, int>
{
    public async Task<int> Handle(ReportarResenaCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var resena = await resenas.ObtenerPorIdAsync(request.IdResena, ct)
            ?? throw new NotFoundException("Reseña", request.IdResena);

        if (await reportes.ExistePendienteAsync(request.IdResena, idUsuario, ct))
            throw new ConflictException("Ya reportaste esta reseña y tu reporte está pendiente de revisión.");

        // El dominio valida que no sea tu propia reseña y que siga visible.
        var reporte = ReportesModeracion.Crear(resena, idUsuario, request.MotivoReporte);

        reportes.Agregar(reporte);
        await unitOfWork.SaveChangesAsync(ct);

        return reporte.IdReporte;
    }
}
