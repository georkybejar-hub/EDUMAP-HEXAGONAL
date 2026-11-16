using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

public sealed record UpdateContactChannelCommand : IRequest<ContactChannelDto?>
{
    public int IdContacto { get; init; }
    public int IdSede { get; init; }
    public string TipoCanal { get; init; } = string.Empty;
    public string ValorContacto { get; init; } = string.Empty;
    public string AreaAtencion { get; init; } = string.Empty;
    public string? HorarioAtencion { get; init; }
}

internal sealed class UpdateContactChannelCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateContactChannelCommand, ContactChannelDto?>
{
    public async Task<ContactChannelDto?> Handle(UpdateContactChannelCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>();

        var entity = await repository.GetByIdAsync(request.IdContacto, cancellationToken);
        if (entity is null) return null;

        entity.IdSede = request.IdSede;
        entity.TipoCanal = request.TipoCanal;
        entity.ValorContacto = request.ValorContacto;
        entity.AreaAtencion = request.AreaAtencion;
        entity.HorarioAtencion = request.HorarioAtencion;

        repository.Update(entity);
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
