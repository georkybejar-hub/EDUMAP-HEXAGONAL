namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IOfertaEducativaRepository
{
    Task<OfertasEducativa?> ObtenerPorIdAsync(int idOferta, CancellationToken ct = default);

    Task<bool> ExisteCombinacionAsync(
        int idSede, int idPrograma, short idModalidad, short idTurno, short anioAcademico,
        CancellationToken ct = default);

    Task<IReadOnlyList<OfertasEducativa>> ListarPorSedeAsync(int idSede, short? anioAcademico, CancellationToken ct = default);

    Task<PagedResult<OfertasEducativa>> FiltrarAsync(FiltroOfertas filtro, CancellationToken ct = default);

    void Agregar(OfertasEducativa oferta);

    void Eliminar(OfertasEducativa oferta);
}
