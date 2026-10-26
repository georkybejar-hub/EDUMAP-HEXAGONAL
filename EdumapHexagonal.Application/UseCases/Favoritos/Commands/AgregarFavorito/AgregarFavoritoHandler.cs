namespace EdumapHexagonal.Application.UseCases.Favoritos.Commands.AgregarFavorito;

public sealed class AgregarFavoritoHandler(
    IInstitucionRepository instituciones,
    IFavoritoRepository favoritos,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AgregarFavoritoCommand>
{
    public async Task Handle(AgregarFavoritoCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        if (!await instituciones.ExisteAsync(request.IdInstitucion, ct))
            throw new NotFoundException("Institución", request.IdInstitucion);

        if (await favoritos.ObtenerAsync(idUsuario, request.IdInstitucion, ct) is not null)
            throw new ConflictException("La institución ya está en tus favoritos.");

        if (await favoritos.ContarPorUsuarioAsync(idUsuario, ct) >= ReglasNegocio.MaximoFavoritos)
            throw new BusinessRuleException(
                $"Puedes guardar como máximo {ReglasNegocio.MaximoFavoritos} instituciones. Quita alguna para agregar otra.");

        favoritos.Agregar(new FavoritosComparador
        {
            IdUsuario = idUsuario,
            IdInstitucion = request.IdInstitucion
        });

        await unitOfWork.SaveChangesAsync(ct);
    }
}
