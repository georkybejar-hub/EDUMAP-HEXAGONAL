namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarAtributosPorCategoria;

public sealed record CategoriaAtributoDto(
    short IdCatAtributo,
    string Nombre,
    string? IconoUi,
    IReadOnlyList<AtributoDto> Atributos);

