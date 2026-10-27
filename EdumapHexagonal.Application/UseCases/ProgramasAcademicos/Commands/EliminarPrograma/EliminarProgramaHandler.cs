namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.EliminarPrograma;

public sealed class EliminarProgramaHandler(
    IProgramaAcademicoRepository programas,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarProgramaCommand>
{
    public async Task Handle(EliminarProgramaCommand request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var programa = await programas.ObtenerPorIdAsync(request.IdPrograma, ct)
            ?? throw new NotFoundException("Programa acadÃ©mico", request.IdPrograma);

        programas.Eliminar(programa);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
