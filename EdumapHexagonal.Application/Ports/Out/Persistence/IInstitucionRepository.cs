namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface IInstitucionRepository
{
    Task<Institucione?> ObtenerPorIdAsync(int idInstitucion, CancellationToken ct = default);

    Task<bool> ExisteAsync(int idInstitucion, CancellationToken ct = default);

    /// <summary>
    /// Incluye: categoría, tipo de gestión, entidad reguladora,
    /// sedes (distrito, canales, ofertas con programa/modalidad/turno),
    /// atributos (atributo y su categoría) y multimedia.
    /// </summary>
    Task<Institucione?> ObtenerDetalleAsync(int idInstitucion, CancellationToken ct = default);

    Task<bool> ExisteCodigoModularAsync(string codigoModular, int? excluirIdInstitucion, CancellationToken ct = default);

    Task<bool> ExisteRucAsync(string ruc, int? excluirIdInstitucion, CancellationToken ct = default);

    /// <summary>Indica si alguna sede de la institución tiene ofertas educativas.</summary>
    Task<bool> TieneOfertasAsync(int idInstitucion, CancellationToken ct = default);

    /// <summary>Incluye categoría y sedes.</summary>
    Task<IReadOnlyList<Institucione>> ListarPorRepresentanteAsync(int idUsuario, CancellationToken ct = default);

    /// <summary>Consulta la vista vw_buscador_instituciones.</summary>
    Task<PagedResult<VwBuscadorInstitucione>> BuscarAsync(FiltroBusquedaInstituciones filtro, CancellationToken ct = default);

    /// <summary>Filas de vw_buscador_instituciones para los ids indicados.</summary>
    Task<IReadOnlyList<VwBuscadorInstitucione>> ObtenerResumenesAsync(IReadOnlyCollection<int> idsInstituciones, CancellationToken ct = default);

    /// <summary>Atributos de varias instituciones. Incluye el atributo y su categoría.</summary>
    Task<IReadOnlyList<InstitucionAtributo>> ListarAtributosAsync(IReadOnlyCollection<int> idsInstituciones, CancellationToken ct = default);

    Task<InstitucionAtributo?> ObtenerAtributoAsync(int idInstitucion, int idAtributo, CancellationToken ct = default);

    /// <summary>Incluye la institución (IdInstitucionNavigation).</summary>
    Task<MultimediaInstitucion?> ObtenerMultimediaAsync(int idMultimedia, CancellationToken ct = default);

    Task<int> ContarMultimediaAsync(int idInstitucion, CancellationToken ct = default);

    void Agregar(Institucione institucion);

    void AgregarAtributo(InstitucionAtributo atributo);

    void EliminarAtributo(InstitucionAtributo atributo);

    void AgregarMultimedia(MultimediaInstitucion multimedia);

    void EliminarMultimedia(MultimediaInstitucion multimedia);
}
