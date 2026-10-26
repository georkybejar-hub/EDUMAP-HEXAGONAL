namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.ListarOfertasPorSede;

/// <summary>Si no se indica año académico, devuelve todos.</summary>
public sealed record ListarOfertasPorSedeQuery(int IdSede, short? AnioAcademico = null)
    : IRequest<IReadOnlyList<OfertaResumenDto>>;
