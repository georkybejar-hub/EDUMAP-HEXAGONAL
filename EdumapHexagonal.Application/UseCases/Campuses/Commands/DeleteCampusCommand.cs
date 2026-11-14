using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

public sealed record DeleteCampusCommand : IRequest<bool>
{
    public int IdSede { get; init; }
}

internal sealed class DeleteCampusCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCampusCommand, bool>
{
    public async Task<bool> Handle(DeleteCampusCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>();

        var entity = await repository.GetByIdAsync(request.IdSede, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
