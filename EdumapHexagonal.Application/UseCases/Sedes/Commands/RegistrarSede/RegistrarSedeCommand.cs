namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.RegistrarSede;

public sealed record RegistrarSedeCommand(
    int IdInstitucion,
    short IdDistrito,
    string NombreSede,
    string Direccion,
    string? Referencia,
    decimal Latitud,
    decimal Longitud,
    bool EsPrincipal = false) : IRequest<int>;
