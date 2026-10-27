namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Commands.CrearPrograma;

public sealed class CrearProgramaHandler(
    IProgramaAcademicoRepository programas,
    ICatalogoRepository catalogos,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CrearProgramaCommand, int>
{
    public async Task<int> Handle(CrearProgramaCommand request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        if (!await catalogos.ExisteCategoriaInstitucionAsync(request.IdCategoria, ct))
            throw new NotFoundException("CategorÃ­a de instituciÃ³n", request.IdCategoria);

        var nombre = request.NombrePrograma.Trim();

        if (await programas.ExisteNombreAsync(request.IdCategoria, nombre, null, ct))
            throw new ConflictException($"Ya existe el programa '{nombre}' en esta categorÃ­a.");

        var programa = new ProgramasAcademico
        {
            IdCategoria = request.IdCategoria,
            NombrePrograma = nombre,
            AreaAcademica = request.AreaAcademica.Trim()
        };

        programas.Agregar(programa);
        await unitOfWork.SaveChangesAsync(ct);

        return programa.IdPrograma;
    }
}
