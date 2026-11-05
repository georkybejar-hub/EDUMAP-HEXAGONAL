using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

internal sealed class DeleteContactChannelCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteContactChannelCommand, bool>
{
    public async Task<bool> Handle(DeleteContactChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.CanalesContactoSede>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
