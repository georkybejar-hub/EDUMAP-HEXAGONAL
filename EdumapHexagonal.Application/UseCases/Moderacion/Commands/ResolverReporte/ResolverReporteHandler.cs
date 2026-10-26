namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ResolverReporte;

public sealed class ResolverReporteHandler(
    IReporteModeracionRepository reportes,
    ICurrentUserService usuarioActual,
    IDateTimeProvider reloj,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ResolverReporteCommand>
{
    public async Task Handle(ResolverReporteCommand request, CancellationToken ct)
    {
        var idAdmin = usuarioActual.RequerirAdministrador();
        var ahora = reloj.Ahora;
        var procede = request.Decision == DecisionModeracion.OcultarResena;

        var reporte = await reportes.ObtenerPorIdAsync(request.IdReporte, ct)
            ?? throw new NotFoundException("Reporte", request.IdReporte);

        // El dominio impide resolver un reporte que ya fue resuelto.
        reporte.Resolver(idAdmin, procede, request.ResolucionAdmin, ahora);

        if (procede)
        {
            reporte.IdResenaNavigation.Ocultar();

            // Si la reseña ya se ocultó, el resto de reportes pendientes sobre ella quedan resueltos.
            var otrosPendientes = await reportes.ListarPendientesPorResenaAsync(reporte.IdResena, ct);
            foreach (var otro in otrosPendientes.Where(r => r.IdReporte != reporte.IdReporte))
                otro.Resolver(idAdmin, true, $"Resuelto junto con el reporte #{reporte.IdReporte}.", ahora);
        }

        await unitOfWork.SaveChangesAsync(ct);
    }
}
