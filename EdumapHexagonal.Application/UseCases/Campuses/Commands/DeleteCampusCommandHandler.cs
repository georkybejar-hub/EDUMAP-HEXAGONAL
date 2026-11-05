using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

internal sealed class DeleteCampusCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCampusCommand, bool>
{
    public async Task<bool> Handle(DeleteCampusCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
