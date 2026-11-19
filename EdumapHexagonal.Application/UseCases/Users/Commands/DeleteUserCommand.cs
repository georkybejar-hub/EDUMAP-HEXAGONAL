using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

public sealed record DeleteUserCommand : IRequest<bool>
{
    public int IdUsuario { get; init; }
}

internal sealed class DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>();

        var entity = await repository.GetByIdAsync(request.IdUsuario, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
