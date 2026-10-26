namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.QuitarAtributo;

public sealed class QuitarAtributoHandler(
    IInstitucionRepository instituciones,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<QuitarAtributoCommand>
{
    public async Task Handle(QuitarAtributoCommand request, CancellationToken ct)
    {
        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("Institución", request.IdInstitucion);

        usuarioActual.ValidarGestionInstitucion(institucion);

        var atributo = await instituciones.ObtenerAtributoAsync(request.IdInstitucion, request.IdAtributo, ct)
            ?? throw new NotFoundException($"La institución no tiene asignado el atributo {request.IdAtributo}.");

        instituciones.EliminarAtributo(atributo);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
