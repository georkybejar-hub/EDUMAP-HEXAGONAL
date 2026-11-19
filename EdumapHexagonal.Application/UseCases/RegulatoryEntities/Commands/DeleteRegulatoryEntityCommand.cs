using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

public sealed record DeleteRegulatoryEntityCommand : IRequest<bool>
{
    public short IdReguladora { get; init; }
}

internal sealed class DeleteRegulatoryEntityCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRegulatoryEntityCommand, bool>
{
    public async Task<bool> Handle(DeleteRegulatoryEntityCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>();

        var entity = await repository.GetByIdAsync(request.IdReguladora, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
