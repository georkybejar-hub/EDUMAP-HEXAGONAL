namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.ListarOfertasPorSede;

public sealed class ListarOfertasPorSedeHandler(
    ISedeRepository sedes,
    IOfertaEducativaRepository ofertas)
    : IRequestHandler<ListarOfertasPorSedeQuery, IReadOnlyList<OfertaResumenDto>>
{
    public async Task<IReadOnlyList<OfertaResumenDto>> Handle(ListarOfertasPorSedeQuery request, CancellationToken ct)
    {
        if (!await sedes.ExisteAsync(request.IdSede, ct))
            throw new NotFoundException("Sede", request.IdSede);

        var lista = await ofertas.ListarPorSedeAsync(request.IdSede, request.AnioAcademico, ct);

        return lista
            .OrderByDescending(o => o.AnioAcademico)
            .ThenBy(o => o.IdProgramaNavigation.NombrePrograma)
            .Select(o => o.ToResumenDto())
            .ToList();
    }
}
