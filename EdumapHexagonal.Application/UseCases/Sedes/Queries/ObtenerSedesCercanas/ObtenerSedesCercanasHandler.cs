namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ObtenerSedesCercanas;

public sealed class ObtenerSedesCercanasHandler(ISedeRepository sedes)
    : IRequestHandler<ObtenerSedesCercanasQuery, IReadOnlyList<SedeCercanaDto>>
{
    public async Task<IReadOnlyList<SedeCercanaDto>> Handle(ObtenerSedesCercanasQuery request, CancellationToken ct)
    {
        var area = CalculadoraDistancia.AreaAlrededor(request.Latitud, request.Longitud, request.RadioKm);
        var candidatas = await sedes.ListarEnAreaAsync(area, request.IdCategoria, ct);

        return candidatas
            .Select(s => new
            {
                Sede = s,
                Distancia = CalculadoraDistancia.DistanciaKm(request.Latitud, request.Longitud, s.Latitud, s.Longitud)
            })
            .Where(x => x.Distancia <= (double)request.RadioKm)
            .OrderBy(x => x.Distancia)
            .Take(request.Maximo)
            .Select(x => new SedeCercanaDto(
                x.Sede.IdSede,
                x.Sede.IdInstitucion,
                x.Sede.IdInstitucionNavigation.Nombre,
                x.Sede.IdInstitucionNavigation.IdCategoriaNavigation.NombreCategoria,
                x.Sede.IdInstitucionNavigation.EstaVerificada(),
                x.Sede.NombreSede,
                x.Sede.Direccion,
                x.Sede.IdDistritoNavigation.NombreDistrito,
                x.Sede.Latitud,
                x.Sede.Longitud,
                Math.Round(x.Distancia, 2)))
            .ToList();
    }
}
