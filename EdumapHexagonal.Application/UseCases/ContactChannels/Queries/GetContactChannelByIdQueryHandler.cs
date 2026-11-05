using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Queries;

internal sealed class GetContactChannelByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetContactChannelByIdQuery, ContactChannelDto?>
{
    public async Task<ContactChannelDto?> Handle(GetContactChannelByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new ContactChannelDto
        {
            IdContacto = e.IdContacto,
            IdSede = e.IdSede,
            TipoCanal = e.TipoCanal,
            ValorContacto = e.ValorContacto,
            AreaAtencion = e.AreaAtencion,
            HorarioAtencion = e.HorarioAtencion
        };
    }
}
