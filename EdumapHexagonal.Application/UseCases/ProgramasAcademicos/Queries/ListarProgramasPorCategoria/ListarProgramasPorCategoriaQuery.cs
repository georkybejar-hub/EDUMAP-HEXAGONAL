namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Queries.ListarProgramasPorCategoria;

public sealed record ListarProgramasPorCategoriaQuery(short IdCategoria, string? AreaAcademica = null)
    : IRequest<IReadOnlyList<ProgramaDto>>;
