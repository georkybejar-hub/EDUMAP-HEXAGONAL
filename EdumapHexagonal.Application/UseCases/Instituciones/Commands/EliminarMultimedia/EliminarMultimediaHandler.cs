namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.EliminarMultimedia;

public sealed class EliminarMultimediaHandler(
    IInstitucionRepository instituciones,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarMultimediaCommand>
{
    public async Task Handle(EliminarMultimediaCommand request, CancellationToken ct)
    {
        var multimedia = await instituciones.ObtenerMultimediaAsync(request.IdMultimedia, ct)
            ?? throw new NotFoundException("Archivo multimedia", request.IdMultimedia);

        usuarioActual.ValidarGestionInstitucion(multimedia.IdInstitucionNavigation);

        instituciones.EliminarMultimedia(multimedia);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
