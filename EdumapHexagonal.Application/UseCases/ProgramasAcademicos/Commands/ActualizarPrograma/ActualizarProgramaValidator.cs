namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.ActualizarPrograma;

public sealed class ActualizarProgramaValidator : AbstractValidator<ActualizarProgramaCommand>
{
    public ActualizarProgramaValidator()
    {
        RuleFor(x => x.IdPrograma).GreaterThan(0);
        RuleFor(x => x.NombrePrograma).NotEmpty().MaximumLength(150);
        RuleFor(x => x.AreaAcademica).NotEmpty().MaximumLength(100);
    }
}
