namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.ActualizarSede;

public sealed record ActualizarSedeCommand(
    int IdSede,
    short IdDistrito,
    string NombreSede,
    string Direccion,
    string? Referencia,
    decimal Latitud,
    decimal Longitud,
    bool EsPrincipal) : IRequest;
