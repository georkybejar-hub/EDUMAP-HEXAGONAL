namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarEntidadesReguladoras;

public sealed record ListarEntidadesReguladorasQuery : IRequest<IReadOnlyList<EntidadReguladoraDto>>;

public sealed record EntidadReguladoraDto(short IdReguladora, string Siglas, string Nombre);
