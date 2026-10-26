namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.EliminarOferta;

public sealed class EliminarOfertaHandler(
    IOfertaEducativaRepository ofertas,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarOfertaCommand>
{
    public async Task Handle(EliminarOfertaCommand request, CancellationToken ct)
    {
        var oferta = await ofertas.ObtenerPorIdAsync(request.IdOferta, ct)
            ?? throw new NotFoundException("Oferta educativa", request.IdOferta);

        usuarioActual.ValidarGestionInstitucion(oferta.IdSedeNavigation.IdInstitucionNavigation);

        ofertas.Eliminar(oferta);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
