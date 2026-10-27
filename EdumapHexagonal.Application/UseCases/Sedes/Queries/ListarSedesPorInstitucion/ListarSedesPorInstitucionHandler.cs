namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ListarSedesPorInstitucion;

public sealed class ListarSedesPorInstitucionHandler(
    IInstitucionRepository instituciones,
    ISedeRepository sedes)
    : IRequestHandler<ListarSedesPorInstitucionQuery, IReadOnlyList<SedeDto>>
{
    public async Task<IReadOnlyList<SedeDto>> Handle(ListarSedesPorInstitucionQuery request, CancellationToken ct)
    {
        if (!await instituciones.ExisteAsync(request.IdInstitucion, ct))
            throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        var lista = await sedes.ListarPorInstitucionAsync(request.IdInstitucion, ct);

        return lista
            .OrderByDescending(s => s.EsSedePrincipal())
            .ThenBy(s => s.NombreSede)
            .Select(s => new SedeDto(
                s.IdSede,
                s.NombreSede,
                s.Direccion,
                s.Referencia,
                s.IdDistrito,
                s.IdDistritoNavigation.NombreDistrito,
                s.Latitud,
                s.Longitud,
                s.EsSedePrincipal(),
                s.CanalesContactoSedes.Select(c => c.ToDto()).ToList()))
            .ToList();
    }
}
