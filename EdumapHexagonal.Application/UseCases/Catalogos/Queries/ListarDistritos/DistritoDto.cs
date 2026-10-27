namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarDistritos;

public sealed record DistritoDto(
    short IdDistrito,
    string Nombre,
    string Ubigeo,
    decimal LatitudCentro,
    decimal LongitudCentro);

