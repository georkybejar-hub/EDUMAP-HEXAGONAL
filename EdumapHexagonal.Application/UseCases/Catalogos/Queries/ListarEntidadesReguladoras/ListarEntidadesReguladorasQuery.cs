namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarEntidadesReguladoras;

public sealed record ListarEntidadesReguladorasQuery : IRequest<IReadOnlyList<EntidadReguladoraDto>>;
