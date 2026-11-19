using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

public sealed record DeleteDistrictCommand : IRequest<bool>
{
    public short IdDistrito { get; init; }
}

internal sealed class DeleteDistrictCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDistrictCommand, bool>
{
    public async Task<bool> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.Distrito>();

        var entity = await repository.GetByIdAsync(request.IdDistrito, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

