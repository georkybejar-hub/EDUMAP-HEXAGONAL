using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;

internal sealed class DeleteEducationalOfferCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteEducationalOfferCommand, bool>
{
    public async Task<bool> Handle(DeleteEducationalOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
