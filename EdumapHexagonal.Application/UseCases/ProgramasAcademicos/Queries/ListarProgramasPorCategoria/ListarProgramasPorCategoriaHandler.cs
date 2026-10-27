namespace EdumapHexagonal.Application.UseCases.ProgramasAcademicos.Queries.ListarProgramasPorCategoria;

public sealed class ListarProgramasPorCategoriaHandler(
    IProgramaAcademicoRepository programas,
    ICatalogoRepository catalogos)
    : IRequestHandler<ListarProgramasPorCategoriaQuery, IReadOnlyList<ProgramaDto>>
{
    public async Task<IReadOnlyList<ProgramaDto>> Handle(ListarProgramasPorCategoriaQuery request, CancellationToken ct)
    {
        if (!await catalogos.ExisteCategoriaInstitucionAsync(request.IdCategoria, ct))
            throw new NotFoundException("CategorÃ­a de instituciÃ³n", request.IdCategoria);

        var area = string.IsNullOrWhiteSpace(request.AreaAcademica) ? null : request.AreaAcademica.Trim();
        var lista = await programas.ListarPorCategoriaAsync(request.IdCategoria, area, ct);

        return lista
            .Select(p => new ProgramaDto(p.IdPrograma, p.NombrePrograma, p.AreaAcademica))
            .ToList();
    }
}
