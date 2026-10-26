namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IProgramaAcademicoRepository
{
    Task<ProgramasAcademico?> ObtenerPorIdAsync(int idPrograma, CancellationToken ct = default);

    /// <summary>Compara sin distinguir mayúsculas dentro de la misma categoría.</summary>
    Task<bool> ExisteNombreAsync(short idCategoria, string nombrePrograma, int? excluirIdPrograma, CancellationToken ct = default);

    /// <summary>Ordenados por nombre. Si se indica área académica, filtra por ella.</summary>
    Task<IReadOnlyList<ProgramasAcademico>> ListarPorCategoriaAsync(short idCategoria, string? areaAcademica, CancellationToken ct = default);

    void Agregar(ProgramasAcademico programa);
}
