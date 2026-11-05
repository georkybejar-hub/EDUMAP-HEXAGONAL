namespace EdumapHexagonal.Application.DTOs.ContactChannels;

public class CreateContactChannelDto
{
    public int IdSede { get; set;}
    public string TipoCanal { get; set;} = string.Empty;
    public string ValorContacto { get; set;} = string.Empty;
    public string AreaAtencion { get; set;} = string.Empty;
    public string? HorarioAtencion { get; set;}
}
