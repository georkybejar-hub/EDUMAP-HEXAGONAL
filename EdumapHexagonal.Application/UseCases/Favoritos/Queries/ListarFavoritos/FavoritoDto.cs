namespace EdumapHexagonal.Application.UseCases.Favoritos.Queries.ListarFavoritos;

public sealed record FavoritoDto(
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
    DateTime? FechaAgregado);
