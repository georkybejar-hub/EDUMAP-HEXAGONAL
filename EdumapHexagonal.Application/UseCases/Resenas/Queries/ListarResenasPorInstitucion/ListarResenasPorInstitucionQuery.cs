namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarResenasPorInstitucion;

public sealed record ListarResenasPorInstitucionQuery(
    int IdInstitucion,
    int Pagina = 1,
    int TamanoPagina = 10) : IRequest<ResenasInstitucionDto>, IConsultaPaginada;
