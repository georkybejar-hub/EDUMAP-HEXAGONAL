using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

internal sealed class DeleteRegulatoryEntityCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteRegulatoryEntityCommand, bool>
{
    public async Task<bool> Handle(DeleteRegulatoryEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
