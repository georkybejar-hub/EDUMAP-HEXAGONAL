namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ObtenerSedesCercanas;

/// <summary>Sedes dentro de un radio (km) alrededor de un punto, de la más cercana a la más lejana.</summary>
public sealed record ObtenerSedesCercanasQuery(
    decimal Latitud,
    decimal Longitud,
    decimal RadioKm = 5,
    short? IdCategoria = null,
    int Maximo = 20) : IRequest<IReadOnlyList<SedeCercanaDto>>;

public sealed record SedeCercanaDto(
    int IdSede,
    int IdInstitucion,
    string Institucion,
    string Categoria,
    bool Verificada,
    string NombreSede,
    string Direccion,
    string Distrito,
    decimal Latitud,
    decimal Longitud,
    double DistanciaKm);
