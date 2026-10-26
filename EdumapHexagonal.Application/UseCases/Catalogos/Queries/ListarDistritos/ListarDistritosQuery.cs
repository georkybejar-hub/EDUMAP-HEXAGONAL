namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarDistritos;

/// <summary>Busqueda opcional por nombre o ubigeo.</summary>
public sealed record ListarDistritosQuery(string? Busqueda = null) : IRequest<IReadOnlyList<DistritoDto>>;

public sealed record DistritoDto(
    short IdDistrito,
    string Nombre,
    string Ubigeo,
    decimal LatitudCentro,
    decimal LongitudCentro);
