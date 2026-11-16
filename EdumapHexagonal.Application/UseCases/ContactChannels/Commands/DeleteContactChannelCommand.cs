using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

public sealed record DeleteContactChannelCommand : IRequest<bool>
{
    public int IdContacto { get; init; }
}

internal sealed class DeleteContactChannelCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteContactChannelCommand, bool>
{
    public async Task<bool> Handle(DeleteContactChannelCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>();

        var entity = await repository.GetByIdAsync(request.IdContacto, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
