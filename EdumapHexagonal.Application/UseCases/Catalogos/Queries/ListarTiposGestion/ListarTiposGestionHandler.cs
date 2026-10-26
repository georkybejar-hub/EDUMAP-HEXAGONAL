namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarTiposGestion;

public sealed class ListarTiposGestionHandler(ICatalogoRepository catalogos)
    : IRequestHandler<ListarTiposGestionQuery, IReadOnlyList<CatalogoItemDto>>
{
    public async Task<IReadOnlyList<CatalogoItemDto>> Handle(ListarTiposGestionQuery request, CancellationToken ct)
    {
        var tipos = await catalogos.ListarTiposGestionAsync(ct);

        return tipos.Select(t => new CatalogoItemDto(t.IdTipoGestion, t.NombreGestion)).ToList();
    }
}
