namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarModalidades;

public sealed class ListarModalidadesHandler(ICatalogoRepository catalogos)
    : IRequestHandler<ListarModalidadesQuery, IReadOnlyList<CatalogoItemDto>>
{
    public async Task<IReadOnlyList<CatalogoItemDto>> Handle(ListarModalidadesQuery request, CancellationToken ct)
    {
        var modalidades = await catalogos.ListarModalidadesAsync(ct);

        return modalidades.Select(m => new CatalogoItemDto(m.IdModalidad, m.NombreModalidad)).ToList();
    }
}
