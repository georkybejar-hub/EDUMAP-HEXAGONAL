namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarTiposGestion;

public sealed record ListarTiposGestionQuery : IRequest<IReadOnlyList<CatalogoItemDto>>;
