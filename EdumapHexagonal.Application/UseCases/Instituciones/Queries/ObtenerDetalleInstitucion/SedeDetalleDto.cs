namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ObtenerDetalleInstitucion;

public sealed record SedeDetalleDto(
    int IdSede,
    string NombreSede,
    string Direccion,
    string? Referencia,
    string Distrito,
    decimal Latitud,
    decimal Longitud,
    bool EsPrincipal,
    IReadOnlyList<CanalContactoDto> Canales,
    IReadOnlyList<OfertaResumenDto> Ofertas);

