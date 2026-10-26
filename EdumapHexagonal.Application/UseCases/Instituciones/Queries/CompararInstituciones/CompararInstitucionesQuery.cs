namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.CompararInstituciones;

/// <summary>Compara entre 2 y 4 instituciones lado a lado.</summary>
public sealed record CompararInstitucionesQuery(IReadOnlyList<int> IdsInstituciones) : IRequest<ComparacionDto>;
