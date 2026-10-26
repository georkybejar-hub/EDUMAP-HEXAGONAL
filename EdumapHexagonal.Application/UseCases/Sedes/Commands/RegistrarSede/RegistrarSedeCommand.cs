namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.RegistrarSede;

/// <summary>
/// La primera sede de una institución siempre queda como principal.
/// Si se marca una nueva sede como principal, la anterior deja de serlo.
/// </summary>
/// <returns>Id de la sede creada.</returns>
public sealed record RegistrarSedeCommand(
    int IdInstitucion,
    short IdDistrito,
    string NombreSede,
    string Direccion,
    string? Referencia,
    decimal Latitud,
    decimal Longitud,
    bool EsPrincipal = false) : IRequest<int>;
