namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ListarSedesPorInstitucion;

public sealed record SedeDto(
    int IdSede,
    string NombreSede,
    string Direccion,
    string? Referencia,
    short IdDistrito,
    string Distrito,
    decimal Latitud,
    decimal Longitud,
    bool EsPrincipal,
    IReadOnlyList<CanalContactoDto> Canales);

