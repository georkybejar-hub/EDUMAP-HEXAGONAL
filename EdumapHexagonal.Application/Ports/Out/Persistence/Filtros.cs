namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public sealed record FiltroUsuarios(
    string? Busqueda,
    short? IdRol,
    bool? Activo,
    int Pagina,
    int TamanoPagina);

public enum OrdenBusquedaInstituciones
{
    Relevancia,
    Nombre,
    MejorCalificadas,
    MenorPension,
    MasPopulares
}

public sealed record FiltroBusquedaInstituciones(
    string? Texto,
    short? IdCategoria,
    short? IdTipoGestion,
    short? IdDistrito,
    decimal? PensionMaxima,
    decimal? CalificacionMinima,
    OrdenBusquedaInstituciones Orden,
    int Pagina,
    int TamanoPagina);

public sealed record FiltroOfertas(
    decimal? PensionMinima,
    decimal? PensionMaxima,
    short? IdModalidad,
    short? IdTurno,
    int? IdPrograma,
    short? IdCategoria,
    short? IdDistrito,
    short? AnioAcademico,
    int Pagina,
    int TamanoPagina);
