namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarModalidades;

public sealed record ListarModalidadesQuery : IRequest<IReadOnlyList<CatalogoItemDto>>;
