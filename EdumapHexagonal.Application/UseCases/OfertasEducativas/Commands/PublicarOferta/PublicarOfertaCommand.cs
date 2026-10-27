namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.PublicarOferta;

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
