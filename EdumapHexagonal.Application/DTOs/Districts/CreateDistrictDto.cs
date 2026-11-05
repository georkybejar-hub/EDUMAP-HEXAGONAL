namespace EdumapHexagonal.Application.DTOs.Districts;

public class CreateDistrictDto
{
    public string NombreDistrito { get; set;} = string.Empty;
    public string Ubigeo { get; set;} = string.Empty;
    public decimal LatitudCentro { get; set;}
    public decimal LongitudCentro { get; set;}
}
