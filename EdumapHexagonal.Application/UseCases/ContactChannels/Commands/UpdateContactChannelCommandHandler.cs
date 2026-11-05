using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

internal sealed class UpdateContactChannelCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateContactChannelCommand, ContactChannelDto?>
{
    public async Task<ContactChannelDto?> Handle(UpdateContactChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>().GetByIdAsync(request.IdContacto);
        if (entity == null) return null;

        entity.IdSede = request.IdSede;
        entity.TipoCanal = request.TipoCanal;
        entity.ValorContacto = request.ValorContacto;
        entity.AreaAtencion = request.AreaAtencion;
        entity.HorarioAtencion = request.HorarioAtencion;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>().Update(entity);
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
