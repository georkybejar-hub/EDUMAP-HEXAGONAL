namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface ISedeRepository
{
    /// <summary>Incluye la institución (IdInstitucionNavigation).</summary>
    Task<SedesInstitucion?> ObtenerPorIdAsync(int idSede, CancellationToken ct = default);

    Task<bool> ExisteAsync(int idSede, CancellationToken ct = default);

    Task<int> ContarPorInstitucionAsync(int idInstitucion, CancellationToken ct = default);

    Task<SedesInstitucion?> ObtenerPrincipalAsync(int idInstitucion, CancellationToken ct = default);

    /// <summary>Incluye distrito y canales de contacto.</summary>
    Task<IReadOnlyList<SedesInstitucion>> ListarPorInstitucionAsync(int idInstitucion, CancellationToken ct = default);

    /// <summary>Sedes dentro del rectángulo. Incluye institución (con categoría) y distrito.</summary>
    Task<IReadOnlyList<SedesInstitucion>> ListarEnAreaAsync(AreaGeografica area, short? idCategoria, CancellationToken ct = default);

    Task<bool> TieneOfertasAsync(int idSede, CancellationToken ct = default);

    /// <summary>Incluye la sede y su institución.</summary>
    Task<CanalesContactoSede?> ObtenerCanalAsync(int idContacto, CancellationToken ct = default);

    Task<bool> ExisteCanalAsync(int idSede, string tipoCanal, string valorContacto, CancellationToken ct = default);

    void Agregar(SedesInstitucion sede);

    void Eliminar(SedesInstitucion sede);

    void AgregarCanal(CanalesContactoSede canal);

    void EliminarCanal(CanalesContactoSede canal);
}
