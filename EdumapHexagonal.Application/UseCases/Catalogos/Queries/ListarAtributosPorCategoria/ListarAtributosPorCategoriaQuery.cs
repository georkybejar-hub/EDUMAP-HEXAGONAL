namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarAtributosPorCategoria;

public sealed record ListarAtributosPorCategoriaQuery(short? IdCatAtributo = null)
    : IRequest<IReadOnlyList<CategoriaAtributoDto>>;
