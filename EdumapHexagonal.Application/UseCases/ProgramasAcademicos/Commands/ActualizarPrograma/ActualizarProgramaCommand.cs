namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.ActualizarPrograma;

/// <summary>
/// Solo administradores. La categoría no se puede cambiar
/// porque las ofertas existentes dependen de ella.
/// </summary>
public sealed record ActualizarProgramaCommand(int IdPrograma, string NombrePrograma, string AreaAcademica) : IRequest;
