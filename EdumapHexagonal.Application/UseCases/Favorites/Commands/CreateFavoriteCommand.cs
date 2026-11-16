using MediatR;
using EdumapHexagonal.Application.DTOs.Favorites;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Favorites.Commands;

public sealed record CreateFavoriteCommand : IRequest<FavoriteDto>
{
    public int IdUsuario { get; init; }
    public int IdInstitucion { get; init; }
}

internal sealed class CreateFavoriteCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateFavoriteCommand, FavoriteDto>
{
    public async Task<FavoriteDto> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.FavoritosComparador>();

        var existing = await repository.FirstOrDefaultAsync(
            f => f.IdUsuario == request.IdUsuario && f.IdInstitucion == request.IdInstitucion,
            cancellationToken);

        if (existing is not null)
        {
            return new FavoriteDto
            {
                IdUsuario = existing.IdUsuario,
                IdInstitucion = existing.IdInstitucion,
                FechaAgregado = existing.FechaAgregado
            };
        }

        var entity = new EdumapHexagonal.Infrastructure.FavoritosComparador
        {
            IdUsuario = request.IdUsuario,
            IdInstitucion = request.IdInstitucion,
            FechaAgregado = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new FavoriteDto
        {
            IdUsuario = entity.IdUsuario,
            IdInstitucion = entity.IdInstitucion,
            FechaAgregado = entity.FechaAgregado
        };
    }
}
