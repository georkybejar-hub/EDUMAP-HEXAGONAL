namespace EdumapHexagonal.Application.UseCases.Favoritos.Commands.QuitarFavorito;

public sealed class QuitarFavoritoHandler(
    IFavoritoRepository favoritos,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<QuitarFavoritoCommand>
{
    public async Task Handle(QuitarFavoritoCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var favorito = await favoritos.ObtenerAsync(idUsuario, request.IdInstitucion, ct)
            ?? throw new NotFoundException("La institución no está en tus favoritos.");

        favoritos.Eliminar(favorito);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
