namespace EdumapHexagonal.Application.DTOs.EducationalOffers;

public sealed record EducationalOfferDto
{
    public int IdOferta { get; init; }
    public int IdSede { get; init; }
    public int IdPrograma { get; init; }
    public short IdModalidad { get; init; }
    public short IdTurno { get; init; }
    public decimal CostoInscripcion { get; init; }
    public decimal CostoMatricula { get; init; }
    public decimal PensionMensual { get; init; }
    public short CuotasAnuales { get; init; }
    public short AnioAcademico { get; init; }
}
