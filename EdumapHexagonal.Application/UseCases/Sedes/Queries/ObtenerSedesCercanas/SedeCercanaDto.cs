namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ObtenerSedesCercanas;

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

