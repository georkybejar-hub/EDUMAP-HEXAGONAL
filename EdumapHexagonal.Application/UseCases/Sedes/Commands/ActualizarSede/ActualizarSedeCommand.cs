namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.ActualizarSede;

/// <summary>
/// Para cambiar la sede principal, marca otra sede como principal (no se puede desmarcar la actual sin reemplazo).
/// </summary>
public sealed record ActualizarSedeCommand(
    int IdSede,
    short IdDistrito,
    string NombreSede,
    string Direccion,
    string? Referencia,
    decimal Latitud,
    decimal Longitud,
    bool EsPrincipal) : IRequest;
