namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Queries.ListarProgramasPorCategoria;

/// <summary>Opcionalmente filtra por área académica.</summary>
public sealed record ListarProgramasPorCategoriaQuery(short IdCategoria, string? AreaAcademica = null)
    : IRequest<IReadOnlyList<ProgramaDto>>;

public sealed record ProgramaDto(int IdPrograma, string NombrePrograma, string AreaAcademica);
