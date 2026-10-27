namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarCategoriasInstitucion;

public sealed class ListarCategoriasInstitucionHandler(ICatalogoRepository catalogos)
    : IRequestHandler<ListarCategoriasInstitucionQuery, IReadOnlyList<CategoriaInstitucionDto>>
{
    public async Task<IReadOnlyList<CategoriaInstitucionDto>> Handle(
        ListarCategoriasInstitucionQuery request, CancellationToken ct)
    {
        var categorias = await catalogos.ListarCategoriasInstitucionAsync(ct);

        return categorias
            .Select(c => new CategoriaInstitucionDto(c.IdCategoria, c.NombreCategoria, c.NivelEducativo))
            .ToList();
    }
}
