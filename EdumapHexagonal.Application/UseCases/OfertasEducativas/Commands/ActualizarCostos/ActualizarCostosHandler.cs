namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.ActualizarCostos;

public sealed class ActualizarCostosHandler(
    IOfertaEducativaRepository ofertas,
    ICurrentUserService usuarioActual,
    IDateTimeProvider reloj,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ActualizarCostosCommand>
{
    public async Task Handle(ActualizarCostosCommand request, CancellationToken ct)
    {
        var oferta = await ofertas.ObtenerPorIdAsync(request.IdOferta, ct)
            ?? throw new NotFoundException("Oferta educativa", request.IdOferta);

        usuarioActual.ValidarGestionInstitucion(oferta.IdSedeNavigation.IdInstitucionNavigation);

        if (oferta.EsDeAnioPasado(reloj.Ahora.Year))
            throw new BusinessRuleException("No se pueden modificar los costos de una oferta de un aÃ±o acadÃ©mico pasado.");

        oferta.ActualizarCostos(
            request.CostoInscripcion,
            request.CostoMatricula,
            request.PensionMensual,
            request.CuotasAnuales);

        await unitOfWork.SaveChangesAsync(ct);
    }
}
