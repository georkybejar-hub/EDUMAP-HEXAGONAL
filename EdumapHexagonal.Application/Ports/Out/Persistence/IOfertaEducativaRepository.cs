namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IOfertaEducativaRepository
{
    /// <summary>Incluye la sede y su institución.</summary>
    Task<OfertasEducativa?> ObtenerPorIdAsync(int idOferta, CancellationToken ct = default);

    Task<bool> ExisteCombinacionAsync(
        int idSede, int idPrograma, short idModalidad, short idTurno, short anioAcademico,
        CancellationToken ct = default);

    /// <summary>Incluye programa, modalidad y turno.</summary>
    Task<IReadOnlyList<OfertasEducativa>> ListarPorSedeAsync(int idSede, short? anioAcademico, CancellationToken ct = default);

    /// <summary>
    /// Ordenadas por pensión ascendente.
    /// Incluye sede (institución y distrito), programa, modalidad y turno.
    /// </summary>
    Task<PagedResult<OfertasEducativa>> FiltrarAsync(FiltroOfertas filtro, CancellationToken ct = default);

    void Agregar(OfertasEducativa oferta);

    void Eliminar(OfertasEducativa oferta);
}
