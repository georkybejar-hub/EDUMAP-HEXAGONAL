namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.SubirMultimedia;

/// <summary>
/// Registra una imagen o video de la institución.
/// El archivo ya debe estar alojado (CDN, almacenamiento en la nube); aquí se guarda su URL.
/// Si no se indica orden, se coloca al final.
/// </summary>
/// <returns>Id del registro multimedia.</returns>
public sealed record SubirMultimediaCommand(
    int IdInstitucion,
    string UrlArchivo,
    string TipoMedio,
    string TituloImagen,
    short? OrdenVisual = null) : IRequest<int>;
