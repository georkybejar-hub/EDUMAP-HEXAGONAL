namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.CompararInstituciones;

public sealed record InstitucionComparadaDto(
    int IdInstitucion,
    string Nombre,
    string? Categoria,
    string? TipoGestion,
    string? EnteRegulador,
    string? Distrito,
    string? Direccion,
    decimal? Latitud,
    decimal? Longitud,
    decimal? PensionMinima,
    decimal? PensionMaxima,
    decimal CalificacionPromedio,
    long TotalResenas,
    long TotalGuardados,
    IReadOnlyList<AtributoInstitucionDto> Atributos);
