namespace EdumapHexagonal.Application.Common.Dtos;

public sealed record OfertaResumenDto(
    int IdOferta,
    int IdPrograma,
    string Programa,
    string Modalidad,
    string Turno,
    short AnioAcademico,
    decimal CostoInscripcion,
    decimal CostoMatricula,
    decimal PensionMensual,
    short CuotasAnuales,
    decimal CostoAnualEstimado);
