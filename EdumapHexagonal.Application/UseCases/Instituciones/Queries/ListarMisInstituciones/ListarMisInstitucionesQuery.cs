namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ListarMisInstituciones;

public sealed record ListarMisInstitucionesQuery : IRequest<IReadOnlyList<MiInstitucionDto>>;
