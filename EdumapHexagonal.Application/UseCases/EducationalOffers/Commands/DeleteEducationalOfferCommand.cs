using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;

public sealed record DeleteEducationalOfferCommand : IRequest<bool>
{
    public int IdOferta { get; init; }
}

internal sealed class DeleteEducationalOfferCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEducationalOfferCommand, bool>
{
    public async Task<bool> Handle(DeleteEducationalOfferCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>();

        var entity = await repository.GetByIdAsync(request.IdOferta, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
