namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.CrearPrograma;

public sealed class CrearProgramaValidator : AbstractValidator<CrearProgramaCommand>
{
    public CrearProgramaValidator()
    {
        RuleFor(x => x.IdCategoria).GreaterThan((short)0);
        RuleFor(x => x.NombrePrograma).NotEmpty().MaximumLength(150);
        RuleFor(x => x.AreaAcademica).NotEmpty().MaximumLength(100);
    }
}
