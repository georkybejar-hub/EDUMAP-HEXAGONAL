namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.ActualizarPrograma;

public sealed record ActualizarProgramaCommand(int IdPrograma, string NombrePrograma, string AreaAcademica) : IRequest;
