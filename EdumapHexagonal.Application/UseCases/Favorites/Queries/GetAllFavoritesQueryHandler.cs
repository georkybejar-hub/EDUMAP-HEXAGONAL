using MediatR;
using EdumapHexagonal.Application.DTOs.Favorites;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Favorites.Queries;

internal sealed class GetAllFavoritesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllFavoritesQuery, IEnumerable<FavoriteDto>>
{
    public async Task<IEnumerable<FavoriteDto>> Handle(GetAllFavoritesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.FavoritosComparador>().GetAllAsync();
        return entities.Select(e => new FavoriteDto
        {
            IdUsuario = e.IdUsuario,
            IdInstitucion = e.IdInstitucion,
            FechaAgregado = e.FechaAgregado
        });
    }
}
