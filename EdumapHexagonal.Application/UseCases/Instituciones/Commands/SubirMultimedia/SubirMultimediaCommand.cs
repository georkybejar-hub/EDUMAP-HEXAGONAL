namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.SubirMultimedia;

public sealed record SubirMultimediaCommand(
    int IdInstitucion,
    string UrlArchivo,
    string TipoMedio,
    string TituloImagen,
    short? OrdenVisual = null) : IRequest<int>;
