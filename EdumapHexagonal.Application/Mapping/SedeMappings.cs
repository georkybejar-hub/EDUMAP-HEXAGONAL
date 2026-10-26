namespace EdumapHexagonal.Application.Mapping;

public static class SedeMappings
{
    public static CanalContactoDto ToDto(this CanalesContactoSede c) => new(
        c.IdContacto,
        c.TipoCanal,
        c.ValorContacto,
        c.AreaAtencion,
        c.HorarioAtencion);
}
