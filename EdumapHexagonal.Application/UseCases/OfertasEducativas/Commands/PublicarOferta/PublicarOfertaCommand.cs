namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.PublicarOferta;

/// <summary>
/// Publica una oferta (programa + modalidad + turno + año) con sus costos en una sede.
/// El programa debe pertenecer a la misma categoría que la institución.
/// </summary>
/// <returns>Id de la oferta creada.</returns>
public sealed record PublicarOfertaCommand(
    int IdSede,
    int IdPrograma,
    short IdModalidad,
    short IdTurno,
    short AnioAcademico,
    decimal CostoInscripcion,
    decimal CostoMatricula,
    decimal PensionMensual,
    short CuotasAnuales = 10) : IRequest<int>;
