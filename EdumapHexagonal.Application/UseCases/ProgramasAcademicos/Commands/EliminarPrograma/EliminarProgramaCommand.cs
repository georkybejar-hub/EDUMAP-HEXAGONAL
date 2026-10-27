namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.EliminarPrograma;

public sealed record EliminarProgramaCommand(int IdPrograma) : IRequest;

