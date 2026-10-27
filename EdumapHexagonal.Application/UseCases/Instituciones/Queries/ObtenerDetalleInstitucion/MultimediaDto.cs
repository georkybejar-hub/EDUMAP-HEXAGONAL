namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ObtenerDetalleInstitucion;

public sealed record MultimediaDto(
    int IdMultimedia,
    string UrlArchivo,
    string TipoMedio,
    string Titulo,
    short Orden);

