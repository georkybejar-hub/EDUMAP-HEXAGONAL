namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.CrearInstitucion;

/// <summary>
/// Registra una institución (queda sin verificar).
/// Representantes: quedan asignados como representante automáticamente.
/// Administradores: pueden indicar el representante en <see cref="IdRepresentante"/>.
/// </summary>
/// <returns>Id de la institución creada.</returns>
public sealed record CrearInstitucionCommand(
    short IdCategoria,
    short IdTipoGestion,
    short? IdReguladora,
    string Nombre,
    string? CodigoModular,
    string? Ruc,
    string Descripcion,
    string? SitioWeb,
    string? LogoUrl,
    int? IdRepresentante = null) : IRequest<int>;
