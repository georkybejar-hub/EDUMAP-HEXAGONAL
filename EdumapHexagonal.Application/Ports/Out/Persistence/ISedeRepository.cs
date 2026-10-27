namespace EdumapHexagonal.Application.Ports.Out.Persistence;

public interface ISedeRepository
{
    Task<SedesInstitucion?> ObtenerPorIdAsync(int idSede, CancellationToken ct = default);

    Task<bool> ExisteAsync(int idSede, CancellationToken ct = default);

    Task<int> ContarPorInstitucionAsync(int idInstitucion, CancellationToken ct = default);

    Task<SedesInstitucion?> ObtenerPrincipalAsync(int idInstitucion, CancellationToken ct = default);

    Task<IReadOnlyList<SedesInstitucion>> ListarPorInstitucionAsync(int idInstitucion, CancellationToken ct = default);

    Task<IReadOnlyList<SedesInstitucion>> ListarEnAreaAsync(AreaGeografica area, short? idCategoria, CancellationToken ct = default);

    Task<bool> TieneOfertasAsync(int idSede, CancellationToken ct = default);

    Task<CanalesContactoSede?> ObtenerCanalAsync(int idContacto, CancellationToken ct = default);

    Task<bool> ExisteCanalAsync(int idSede, string tipoCanal, string valorContacto, CancellationToken ct = default);

    void Agregar(SedesInstitucion sede);

    void Eliminar(SedesInstitucion sede);

    void AgregarCanal(CanalesContactoSede canal);

    void EliminarCanal(CanalesContactoSede canal);
}
