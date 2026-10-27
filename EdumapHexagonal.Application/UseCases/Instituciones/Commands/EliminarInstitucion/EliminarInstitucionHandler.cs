namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.EliminarInstitucion;

public sealed class EliminarInstitucionHandler(
    IInstitucionRepository instituciones,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarInstitucionCommand>
{
    public async Task Handle(EliminarInstitucionCommand request, CancellationToken ct)
    {
        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        usuarioActual.ValidarGestionInstitucion(institucion);

        if (await instituciones.TieneOfertasAsync(request.IdInstitucion, ct))
            throw new BusinessRuleException("No se puede eliminar la instituciÃ³n porque tiene ofertas acadÃ©micas asociadas.");

        instituciones.Eliminar(institucion);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
