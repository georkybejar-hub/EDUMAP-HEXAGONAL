namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarEntidadesReguladoras;

public sealed class ListarEntidadesReguladorasHandler(ICatalogoRepository catalogos)
    : IRequestHandler<ListarEntidadesReguladorasQuery, IReadOnlyList<EntidadReguladoraDto>>
{
    public async Task<IReadOnlyList<EntidadReguladoraDto>> Handle(
        ListarEntidadesReguladorasQuery request, CancellationToken ct)
    {
        var entidades = await catalogos.ListarEntidadesReguladorasAsync(ct);

        return entidades
            .Select(e => new EntidadReguladoraDto(e.IdReguladora, e.Siglas, e.NombreEntidad))
            .ToList();
    }
}
