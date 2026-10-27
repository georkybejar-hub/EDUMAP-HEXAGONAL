using EdumapHexagonal.Application.UseCases.Sedes.Queries.ListarSedesPorInstitucion;

namespace EdumapHexagonal.Application.Mapping;

public static class SedeMappings
{
    public static CanalContactoDto ToDto(this CanalesContactoSede c) => new(
        c.IdContacto,
        c.TipoCanal,
        c.ValorContacto,
        c.AreaAtencion,
        c.HorarioAtencion);

    public static SedeDto ToDto(this SedesInstitucion s) => new(
        s.IdSede,
        s.NombreSede,
        s.Direccion,
        s.Referencia,
        s.IdDistrito,
        s.IdDistritoNavigation?.NombreDistrito ?? string.Empty,
        s.Latitud,
        s.Longitud,
        s.EsSedePrincipal(),
        s.CanalesContactoSedes?.Select(c => c.ToDto()).ToList() ?? new List<CanalContactoDto>());
}
