namespace EdumapHexagonal.Application.DTOs.Institutions;

public class InstitutionDto
{
    public int IdInstitucion { get; set;}
    public short IdCategoria { get; set;}
    public short IdTipoGestion { get; set;}
    public short? IdReguladora { get; set;}
    public int? IdRepresentante { get; set;}
    public string Nombre { get; set;} = string.Empty;
    public string? CodigoModular { get; set;}
    public string? Ruc { get; set;}
    public string Descripcion { get; set;} = string.Empty;
    public string? SitioWeb { get; set;}
    public string? LogoUrl { get; set;}
    public bool? Verificada { get; set;}
    public DateTime? FechaCreacion { get; set;}
}
