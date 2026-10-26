namespace EdumapHexagonal.Application.UseCases.Favoritos.Queries.ListarFavoritos;

/// <summary>Favoritos del usuario autenticado, los más recientes primero.</summary>
public sealed record ListarFavoritosQuery : IRequest<IReadOnlyList<FavoritoDto>>;

public sealed record FavoritoDto(InstitucionResumenDto Institucion, DateTime? FechaAgregado);
