namespace EdumapHexagonal.Application.UseCases.Favoritos.Queries.ListarFavoritos;

public sealed class ListarFavoritosHandler(
    IFavoritoRepository favoritos,
    IInstitucionRepository instituciones,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ListarFavoritosQuery, IReadOnlyList<FavoritoDto>>
{
    public async Task<IReadOnlyList<FavoritoDto>> Handle(ListarFavoritosQuery request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var lista = await favoritos.ListarPorUsuarioAsync(idUsuario, ct);
        if (lista.Count == 0)
            return [];

        // Las tarjetas salen de la vista del buscador (calificación, pensiones, etc. ya calculadas).
        var resumenes = (await instituciones.ObtenerResumenesAsync(lista.Select(f => f.IdInstitucion).ToList(), ct))
            .Where(v => v.IdInstitucion is not null)
            .ToDictionary(v => v.IdInstitucion!.Value);

        return lista
            .Where(f => resumenes.ContainsKey(f.IdInstitucion))
            .Select(f => new FavoritoDto(resumenes[f.IdInstitucion].ToResumenDto(), f.FechaAgregado))
            .ToList();
    }
}
