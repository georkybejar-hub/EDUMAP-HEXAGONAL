namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.CrearPrograma;

public sealed record CrearProgramaCommand(short IdCategoria, string NombrePrograma, string AreaAcademica) : IRequest<int>;
