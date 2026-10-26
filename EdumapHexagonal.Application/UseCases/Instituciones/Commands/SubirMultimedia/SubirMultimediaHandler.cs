namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.SubirMultimedia;

public sealed class SubirMultimediaHandler(
    IInstitucionRepository instituciones,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SubirMultimediaCommand, int>
{
    public async Task<int> Handle(SubirMultimediaCommand request, CancellationToken ct)
    {
        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("Institución", request.IdInstitucion);

        usuarioActual.ValidarGestionInstitucion(institucion);

        var cantidadActual = await instituciones.ContarMultimediaAsync(request.IdInstitucion, ct);
        if (cantidadActual >= ReglasNegocio.MaximoMultimediaPorInstitucion)
            throw new BusinessRuleException(
                $"La institución ya tiene el máximo de {ReglasNegocio.MaximoMultimediaPorInstitucion} archivos multimedia.");

        var multimedia = new MultimediaInstitucion
        {
            IdInstitucion = request.IdInstitucion,
            UrlArchivo = request.UrlArchivo.Trim(),
            TipoMedio = TiposMedio.Normalizar(request.TipoMedio),
            TituloImagen = request.TituloImagen.Trim(),
            OrdenVisual = request.OrdenVisual ?? (short)(cantidadActual + 1)
        };

        instituciones.AgregarMultimedia(multimedia);
        await unitOfWork.SaveChangesAsync(ct);

        return multimedia.IdMultimedia;
    }
}
