namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarDistritos;

public sealed class ListarDistritosHandler(ICatalogoRepository catalogos)
    : IRequestHandler<ListarDistritosQuery, IReadOnlyList<DistritoDto>>
{
    public async Task<IReadOnlyList<DistritoDto>> Handle(ListarDistritosQuery request, CancellationToken ct)
    {
        var busqueda = string.IsNullOrWhiteSpace(request.Busqueda) ? null : request.Busqueda.Trim();
        var distritos = await catalogos.ListarDistritosAsync(busqueda, ct);

        return distritos
            .Select(d => new DistritoDto(d.IdDistrito, d.NombreDistrito, d.Ubigeo, d.LatitudCentro, d.LongitudCentro))
            .ToList();
    }
}
