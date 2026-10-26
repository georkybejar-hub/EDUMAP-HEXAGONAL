namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarResenasPorInstitucion;

/// <summary>Reseñas visibles (paginadas) junto con el resumen de calificaciones.</summary>
public sealed record ListarResenasPorInstitucionQuery(
    int IdInstitucion,
    int Pagina = 1,
    int TamanoPagina = 10) : IRequest<ResenasInstitucionDto>, IConsultaPaginada;
