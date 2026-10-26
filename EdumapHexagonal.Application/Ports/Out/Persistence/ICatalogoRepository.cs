namespace EdumapHexagonal.Application.Ports.Out.Persistence;

/// <summary>Tablas de catálogo (listas de valores). Todos los listados vienen ordenados por nombre.</summary>
public interface ICatalogoRepository
{
    Task<IReadOnlyList<CategoriasInstitucion>> ListarCategoriasInstitucionAsync(CancellationToken ct = default);

    Task<IReadOnlyList<TiposGestion>> ListarTiposGestionAsync(CancellationToken ct = default);

    Task<IReadOnlyList<EntidadesReguladora>> ListarEntidadesReguladorasAsync(CancellationToken ct = default);

    Task<IReadOnlyList<ModalidadesEstudio>> ListarModalidadesAsync(CancellationToken ct = default);

    Task<IReadOnlyList<TurnosEstudio>> ListarTurnosAsync(CancellationToken ct = default);

    /// <summary>Si se indica búsqueda, filtra por nombre o ubigeo.</summary>
    Task<IReadOnlyList<Distrito>> ListarDistritosAsync(string? busqueda, CancellationToken ct = default);

    /// <summary>Incluye los atributos de cada categoría.</summary>
    Task<IReadOnlyList<CategoriasAtributo>> ListarCategoriasAtributoAsync(short? idCatAtributo, CancellationToken ct = default);

    Task<bool> ExisteCategoriaInstitucionAsync(short idCategoria, CancellationToken ct = default);

    Task<bool> ExisteTipoGestionAsync(short idTipoGestion, CancellationToken ct = default);

    Task<bool> ExisteEntidadReguladoraAsync(short idReguladora, CancellationToken ct = default);

    Task<bool> ExisteModalidadAsync(short idModalidad, CancellationToken ct = default);

    Task<bool> ExisteTurnoAsync(short idTurno, CancellationToken ct = default);

    Task<bool> ExisteDistritoAsync(short idDistrito, CancellationToken ct = default);

    Task<bool> ExisteAtributoAsync(int idAtributo, CancellationToken ct = default);

    Task<bool> ExisteCategoriaAtributoAsync(short idCatAtributo, CancellationToken ct = default);
}
