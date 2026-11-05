using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

internal sealed class DeleteManagementTypeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteManagementTypeCommand, bool>
{
    public async Task<bool> Handle(DeleteManagementTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
