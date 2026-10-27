namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarDistritos;

public sealed record ListarDistritosQuery(string? Busqueda = null) : IRequest<IReadOnlyList<DistritoDto>>;
