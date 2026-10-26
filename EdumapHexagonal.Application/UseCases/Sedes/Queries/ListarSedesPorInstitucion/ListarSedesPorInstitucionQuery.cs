namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ListarSedesPorInstitucion;

/// <summary>La sede principal aparece primero.</summary>
public sealed record ListarSedesPorInstitucionQuery(int IdInstitucion) : IRequest<IReadOnlyList<SedeDto>>;

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
