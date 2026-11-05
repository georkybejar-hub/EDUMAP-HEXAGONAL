namespace EdumapHexagonal.Application.DTOs.EducationalOffers;

public class UpdateEducationalOfferDto
{
    public int IdSede { get; set;}
    public int IdPrograma { get; set;}
    public short IdModalidad { get; set;}
    public short IdTurno { get; set;}
    public decimal CostoInscripcion { get; set;}
    public decimal CostoMatricula { get; set;}
    public decimal PensionMensual { get; set;}
    public short CuotasAnuales { get; set;}
    public short AnioAcademico { get; set;}
}
