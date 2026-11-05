using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

public sealed record UpdateContactChannelCommand : IRequest<ContactChannelDto?>
{
    public int IdContacto { get; set; }
    public int IdSede { get; init; }
    public string TipoCanal { get; init; } = string.Empty;
    public string ValorContacto { get; init; } = string.Empty;
    public string AreaAtencion { get; init; } = string.Empty;
    public string? HorarioAtencion { get; init; }
}
