using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

internal sealed class DeleteDistrictCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteDistrictCommand, bool>
{
    public async Task<bool> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
