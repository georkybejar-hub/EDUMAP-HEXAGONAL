using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Queries;

public sealed record GetContactChannelByIdQuery : IRequest<ContactChannelDto?>
{
    public int IdContacto { get; init; }
}

internal sealed class GetContactChannelByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetContactChannelByIdQuery, ContactChannelDto?>
{
    public async Task<ContactChannelDto?> Handle(GetContactChannelByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.CanalesContactoSede>()
            .GetByIdAsync(request.IdContacto, cancellationToken);

        if (entity is null) return null;

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

