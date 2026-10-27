namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.ListarOfertasPorSede;

public sealed record ListarOfertasPorSedeQuery(int IdSede, short? AnioAcademico = null)
    : IRequest<IReadOnlyList<OfertaResumenDto>>;
