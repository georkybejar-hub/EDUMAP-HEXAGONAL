using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

internal sealed class CreateContactChannelCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateContactChannelCommand, ContactChannelDto>
{
    public async Task<ContactChannelDto> Handle(CreateContactChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.CanalesContactoSede
        {
            IdSede = request.IdSede,
            TipoCanal = request.TipoCanal,
            ValorContacto = request.ValorContacto,
            AreaAtencion = request.AreaAtencion,
            HorarioAtencion = request.HorarioAtencion
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
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
