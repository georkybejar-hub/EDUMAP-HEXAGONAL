using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Reviews.Commands;

public sealed record DeleteReviewCommand : IRequest<bool>
{
    public int IdResena { get; init; }
}

internal sealed class DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteReviewCommand, bool>
{
    public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>();

        var entity = await repository.GetByIdAsync(request.IdResena, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
