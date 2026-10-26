namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarAtributosPorCategoria;

/// <summary>
/// Atributos agrupados por categoría (ej. "Infraestructura" → "Piscina", "Laboratorio").
/// Si no se indica categoría, devuelve todas.
/// </summary>
public sealed record ListarAtributosPorCategoriaQuery(short? IdCatAtributo = null)
    : IRequest<IReadOnlyList<CategoriaAtributoDto>>;

public sealed record CategoriaAtributoDto(
    short IdCatAtributo,
    string Nombre,
    string? IconoUi,
    IReadOnlyList<AtributoDto> Atributos);

public sealed record AtributoDto(int IdAtributo, string Nombre);
