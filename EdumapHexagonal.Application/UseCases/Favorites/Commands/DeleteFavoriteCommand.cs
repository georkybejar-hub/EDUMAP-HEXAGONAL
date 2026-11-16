using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Favorites.Commands;

public sealed record DeleteFavoriteCommand : IRequest<bool>
{
    public int IdUsuario { get; init; }
    public int IdInstitucion { get; init; }
}

internal sealed class DeleteFavoriteCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteFavoriteCommand, bool>
{
    public async Task<bool> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.FavoritosComparador>();

        var entity = await repository.FirstOrDefaultAsync(
            f => f.IdUsuario == request.IdUsuario && f.IdInstitucion == request.IdInstitucion,
            cancellationToken);

        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
