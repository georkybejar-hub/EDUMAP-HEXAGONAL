namespace EdumapHexagonal.Application.DTOs.Campuses;

public class CreateCampusDto
{
    public int IdInstitucion { get; set;}
    public short IdDistrito { get; set;}
    public string NombreSede { get; set;} = string.Empty;
    public string Direccion { get; set;} = string.Empty;
    public string? Referencia { get; set;}
    public decimal Latitud { get; set;}
    public decimal Longitud { get; set;}
    public bool? EsPrincipal { get; set;}
}
