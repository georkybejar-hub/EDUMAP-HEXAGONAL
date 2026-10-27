namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IProgramaAcademicoRepository
{
    Task<ProgramasAcademico?> ObtenerPorIdAsync(int idPrograma, CancellationToken ct = default);

    Task<bool> ExisteNombreAsync(short idCategoria, string nombrePrograma, int? excluirIdPrograma, CancellationToken ct = default);

    Task<IReadOnlyList<ProgramasAcademico>> ListarPorCategoriaAsync(short idCategoria, string? areaAcademica, CancellationToken ct = default);

    void Agregar(ProgramasAcademico programa);

    void Eliminar(ProgramasAcademico programa);
}
