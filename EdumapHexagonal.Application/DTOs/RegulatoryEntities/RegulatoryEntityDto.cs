namespace EdumapHexagonal.Application.DTOs.RegulatoryEntities;

public class RegulatoryEntityDto
{
    public short IdReguladora { get; set;}
    public string Siglas { get; set;} = string.Empty;
    public string NombreEntidad { get; set;} = string.Empty;
}
