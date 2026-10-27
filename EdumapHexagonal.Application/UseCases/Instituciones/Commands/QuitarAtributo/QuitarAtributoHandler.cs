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
            ?? throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        usuarioActual.ValidarGestionInstitucion(institucion);

        var atributo = await instituciones.ObtenerAtributoAsync(request.IdInstitucion, request.IdAtributo, ct)
            ?? throw new NotFoundException($"La instituciÃ³n no tiene asignado el atributo {request.IdAtributo}.");

        instituciones.EliminarAtributo(atributo);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
