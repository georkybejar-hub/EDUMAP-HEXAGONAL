using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

public sealed record CreateContactChannelCommand : IRequest<ContactChannelDto>
{
    public int IdSede { get; init; }
    public string TipoCanal { get; init; } = string.Empty;
    public string ValorContacto { get; init; } = string.Empty;
    public string AreaAtencion { get; init; } = string.Empty;
    public string? HorarioAtencion { get; init; }
}

internal sealed class CreateContactChannelCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateContactChannelCommand, ContactChannelDto>
{
    public async Task<ContactChannelDto> Handle(CreateContactChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.CanalesContactoSede
        {
            IdSede = request.IdSede,
            TipoCanal = request.TipoCanal,
            ValorContacto = request.ValorContacto,
            AreaAtencion = request.AreaAtencion,
            HorarioAtencion = request.HorarioAtencion
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.CanalesContactoSede>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ContactChannelDto
        {
            IdContacto = entity.IdContacto,
            IdSede = entity.IdSede,
            TipoCanal = entity.TipoCanal,
            ValorContacto = entity.ValorContacto,
            AreaAtencion = entity.AreaAtencion,
            HorarioAtencion = entity.HorarioAtencion
        };
    }
}

