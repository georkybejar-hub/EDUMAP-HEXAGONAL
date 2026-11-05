using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

internal sealed class DeleteInstitutionMediaCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteInstitutionMediaCommand, bool>
{
    public async Task<bool> Handle(DeleteInstitutionMediaCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
