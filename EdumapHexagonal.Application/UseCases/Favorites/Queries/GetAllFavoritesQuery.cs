using MediatR;
using EdumapHexagonal.Application.DTOs.Favorites;

namespace EdumapHexagonal.Application.UseCases.Favorites.Queries;

public sealed record GetAllFavoritesQuery : IRequest<IEnumerable<FavoriteDto>>;
