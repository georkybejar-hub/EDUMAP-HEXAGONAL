namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.CrearPrograma;

/// <summary>Solo administradores. Devuelve el id del programa creado.</summary>
public sealed record CrearProgramaCommand(short IdCategoria, string NombrePrograma, string AreaAcademica) : IRequest<int>;
