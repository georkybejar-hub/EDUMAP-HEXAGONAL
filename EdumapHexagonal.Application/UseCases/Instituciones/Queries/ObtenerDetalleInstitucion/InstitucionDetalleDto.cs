namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ObtenerDetalleInstitucion;

public sealed record InstitucionDetalleDto(
    int IdInstitucion,
    string Nombre,
    string Descripcion,
    string Categoria,
    string NivelEducativo,
    string TipoGestion,
    string? EntidadReguladora,
    string? CodigoModular,
    string? Ruc,
    string? SitioWeb,
    string? LogoUrl,
    bool Verificada,
    decimal CalificacionPromedio,
    long TotalResenas,
    IReadOnlyList<SedeDetalleDto> Sedes,
    IReadOnlyList<AtributoInstitucionDto> Atributos,
    IReadOnlyList<MultimediaDto> Multimedia);

public sealed record SedeDetalleDto(
    int IdSede,
    string NombreSede,
    string Direccion,
    string? Referencia,
    string Distrito,
    decimal Latitud,
    decimal Longitud,
    bool EsPrincipal,
    IReadOnlyList<CanalContactoDto> Canales,
    IReadOnlyList<OfertaResumenDto> Ofertas);

public sealed record MultimediaDto(
    int IdMultimedia,
    string UrlArchivo,
    string TipoMedio,
    string Titulo,
    short Orden);
