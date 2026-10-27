namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ObtenerSedesCercanas;

public sealed record ObtenerSedesCercanasQuery(
    decimal Latitud,
    decimal Longitud,
    decimal RadioKm = 5,
    short? IdCategoria = null,
    int Maximo = 20) : IRequest<IReadOnlyList<SedeCercanaDto>>;
