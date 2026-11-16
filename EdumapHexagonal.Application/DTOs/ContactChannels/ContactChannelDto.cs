namespace EdumapHexagonal.Application.DTOs.ContactChannels;

public sealed record ContactChannelDto
{
    public int IdContacto { get; init; }
    public int IdSede { get; init; }
    public string TipoCanal { get; init; } = string.Empty;
    public string ValorContacto { get; init; } = string.Empty;
    public string AreaAtencion { get; init; } = string.Empty;
    public string? HorarioAtencion { get; init; }
}
