namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.ActualizarPrograma;

public sealed class ActualizarProgramaHandler(
    IProgramaAcademicoRepository programas,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ActualizarProgramaCommand>
{
    public async Task Handle(ActualizarProgramaCommand request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var programa = await programas.ObtenerPorIdAsync(request.IdPrograma, ct)
            ?? throw new NotFoundException("Programa acadÃ©mico", request.IdPrograma);

        var nombre = request.NombrePrograma.Trim();

        if (await programas.ExisteNombreAsync(programa.IdCategoria, nombre, programa.IdPrograma, ct))
            throw new ConflictException($"Ya existe otro programa llamado '{nombre}' en esta categorÃ­a.");

        programa.NombrePrograma = nombre;
        programa.AreaAcademica = request.AreaAcademica.Trim();

        await unitOfWork.SaveChangesAsync(ct);
    }
}
