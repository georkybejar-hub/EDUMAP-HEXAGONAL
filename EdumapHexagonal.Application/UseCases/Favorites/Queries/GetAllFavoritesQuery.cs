using MediatR;
using EdumapHexagonal.Application.DTOs.Favorites;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Favorites.Queries;

public sealed record GetAllFavoritesQuery : IRequest<IReadOnlyList<FavoriteDto>>;

internal sealed class GetAllFavoritesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllFavoritesQuery, IReadOnlyList<FavoriteDto>>
{
    public async Task<IReadOnlyList<FavoriteDto>> Handle(GetAllFavoritesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.FavoritosComparador>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new FavoriteDto
            {
                IdUsuario = e.IdUsuario,
                IdInstitucion = e.IdInstitucion,
                FechaAgregado = e.FechaAgregado
            })
            .ToList();
    }
}
