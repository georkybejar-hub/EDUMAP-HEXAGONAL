using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Queries;

public sealed record GetAllContactChannelsQuery : IRequest<IReadOnlyList<ContactChannelDto>>;

internal sealed class GetAllContactChannelsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllContactChannelsQuery, IReadOnlyList<ContactChannelDto>>
{
    public async Task<IReadOnlyList<ContactChannelDto>> Handle(GetAllContactChannelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.CanalesContactoSede>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new ContactChannelDto
            {
                IdContacto = e.IdContacto,
                IdSede = e.IdSede,
                TipoCanal = e.TipoCanal,
                ValorContacto = e.ValorContacto,
                AreaAtencion = e.AreaAtencion,
                HorarioAtencion = e.HorarioAtencion
            })
            .ToList();
    }
}

