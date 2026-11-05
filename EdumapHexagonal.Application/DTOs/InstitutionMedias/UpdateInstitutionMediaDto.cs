namespace EdumapHexagonal.Application.DTOs.InstitutionMedias;

public class UpdateInstitutionMediaDto
{
    public int IdInstitucion { get; set;}
    public string UrlArchivo { get; set;} = string.Empty;
    public string TipoMedio { get; set;} = string.Empty;
    public string TituloImagen { get; set;} = string.Empty;
    public short? OrdenVisual { get; set;}
}
