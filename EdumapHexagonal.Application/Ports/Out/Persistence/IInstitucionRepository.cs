namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IInstitucionRepository
{
    Task<Institucione?> ObtenerPorIdAsync(int idInstitucion, CancellationToken ct = default);

    Task<bool> ExisteAsync(int idInstitucion, CancellationToken ct = default);

    Task<Institucione?> ObtenerDetalleAsync(int idInstitucion, CancellationToken ct = default);

    Task<bool> ExisteCodigoModularAsync(string codigoModular, int? excluirIdInstitucion, CancellationToken ct = default);

    Task<bool> ExisteRucAsync(string ruc, int? excluirIdInstitucion, CancellationToken ct = default);

    Task<bool> TieneOfertasAsync(int idInstitucion, CancellationToken ct = default);

    Task<IReadOnlyList<Institucione>> ListarPorRepresentanteAsync(int idUsuario, CancellationToken ct = default);

    Task<PagedResult<VwBuscadorInstitucione>> BuscarAsync(FiltroBusquedaInstituciones filtro, CancellationToken ct = default);

    Task<IReadOnlyList<VwBuscadorInstitucione>> ObtenerResumenesAsync(IReadOnlyCollection<int> idsInstituciones, CancellationToken ct = default);

    Task<IReadOnlyList<InstitucionAtributo>> ListarAtributosAsync(IReadOnlyCollection<int> idsInstituciones, CancellationToken ct = default);

    Task<InstitucionAtributo?> ObtenerAtributoAsync(int idInstitucion, int idAtributo, CancellationToken ct = default);

    Task<MultimediaInstitucion?> ObtenerMultimediaAsync(int idMultimedia, CancellationToken ct = default);

    Task<int> ContarMultimediaAsync(int idInstitucion, CancellationToken ct = default);

    void Agregar(Institucione institucion);

    void Eliminar(Institucione institucion);

    void AgregarAtributo(InstitucionAtributo atributo);

    void EliminarAtributo(InstitucionAtributo atributo);

    void AgregarMultimedia(MultimediaInstitucion multimedia);

    void EliminarMultimedia(MultimediaInstitucion multimedia);
}
