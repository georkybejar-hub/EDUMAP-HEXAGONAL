using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

internal sealed class DeleteInstitutionCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteInstitutionCommand, bool>
{
    public async Task<bool> Handle(DeleteInstitutionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
