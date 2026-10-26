namespace EdumapHexagonal.Application.Common.Dtos;

public sealed record AtributoInstitucionDto(
    int IdAtributo,
    string Atributo,
    string Categoria,
    string? IconoUi,
    string? Detalle);
