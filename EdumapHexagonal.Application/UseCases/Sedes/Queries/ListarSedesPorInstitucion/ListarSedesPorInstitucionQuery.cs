namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ListarSedesPorInstitucion;

public sealed record ListarSedesPorInstitucionQuery(int IdInstitucion) : IRequest<IReadOnlyList<SedeDto>>;
