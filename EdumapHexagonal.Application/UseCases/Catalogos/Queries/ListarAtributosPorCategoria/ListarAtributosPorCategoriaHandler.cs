namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarAtributosPorCategoria;

public sealed class ListarAtributosPorCategoriaHandler(ICatalogoRepository catalogos)
    : IRequestHandler<ListarAtributosPorCategoriaQuery, IReadOnlyList<CategoriaAtributoDto>>
{
    public async Task<IReadOnlyList<CategoriaAtributoDto>> Handle(
        ListarAtributosPorCategoriaQuery request, CancellationToken ct)
    {
        if (request.IdCatAtributo is { } idCategoria &&
            !await catalogos.ExisteCategoriaAtributoAsync(idCategoria, ct))
            throw new NotFoundException("Categoría de atributo", idCategoria);

        var categorias = await catalogos.ListarCategoriasAtributoAsync(request.IdCatAtributo, ct);

        return categorias
            .Select(c => new CategoriaAtributoDto(
                c.IdCatAtributo,
                c.NombreCatAtributo,
                c.IconoUi,
                c.AtributosCatalogos
                    .OrderBy(a => a.NombreAtributo)
                    .Select(a => new AtributoDto(a.IdAtributo, a.NombreAtributo))
                    .ToList()))
            .ToList();
    }
}
