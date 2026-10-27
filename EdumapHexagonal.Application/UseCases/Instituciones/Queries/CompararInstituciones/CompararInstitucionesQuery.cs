namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.CompararInstituciones;

public sealed record CompararInstitucionesQuery(IReadOnlyList<int> IdsInstituciones) : IRequest<ComparacionDto>;
