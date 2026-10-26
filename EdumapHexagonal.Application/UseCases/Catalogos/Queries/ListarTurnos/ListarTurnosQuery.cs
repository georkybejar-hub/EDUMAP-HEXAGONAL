namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarTurnos;

public sealed record ListarTurnosQuery : IRequest<IReadOnlyList<CatalogoItemDto>>;
