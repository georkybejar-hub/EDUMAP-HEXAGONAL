namespace EdumapHexagonal.Application.DTOs.InstitutionMedias;

public class InstitutionMediaDto
{
    public int IdMultimedia { get; set;}
    public int IdInstitucion { get; set;}
    public string UrlArchivo { get; set;} = string.Empty;
    public string TipoMedio { get; set;} = string.Empty;
    public string TituloImagen { get; set;} = string.Empty;
    public short? OrdenVisual { get; set;}
}
