namespace EdumapHexagonal.Application.UseCases.Favoritos.Queries.ListarFavoritos;

public sealed record ListarFavoritosQuery : IRequest<IReadOnlyList<FavoritoDto>>;
