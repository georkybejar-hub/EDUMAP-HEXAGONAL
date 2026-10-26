namespace EdumapHexagonal.Application.Common.Dtos;

/// <summary>Tarjeta de institución (buscador, comparador y favoritos). Sale de vw_buscador_instituciones.</summary>
public sealed record InstitucionResumenDto(
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
    long TotalGuardados);
