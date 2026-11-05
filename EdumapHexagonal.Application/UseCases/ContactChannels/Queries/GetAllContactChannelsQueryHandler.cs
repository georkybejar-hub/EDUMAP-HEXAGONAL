using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Queries;

internal sealed class GetAllContactChannelsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllContactChannelsQuery, IEnumerable<ContactChannelDto>>
{
    public async Task<IEnumerable<ContactChannelDto>> Handle(GetAllContactChannelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>().GetAllAsync();
        return entities.Select(e => new ContactChannelDto
        {
            IdContacto = e.IdContacto,
            IdSede = e.IdSede,
            TipoCanal = e.TipoCanal,
            ValorContacto = e.ValorContacto,
            AreaAtencion = e.AreaAtencion,
            HorarioAtencion = e.HorarioAtencion
        });
    }
}
