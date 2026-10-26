namespace EdumapHexagonal.Application.Common.Dtos;

public sealed record CanalContactoDto(
    int IdContacto,
    string TipoCanal,
    string ValorContacto,
    string AreaAtencion,
    string? HorarioAtencion);
