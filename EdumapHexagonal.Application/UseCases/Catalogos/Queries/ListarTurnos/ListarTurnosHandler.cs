namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarTurnos;

public sealed class ListarTurnosHandler(ICatalogoRepository catalogos)
    : IRequestHandler<ListarTurnosQuery, IReadOnlyList<CatalogoItemDto>>
{
    public async Task<IReadOnlyList<CatalogoItemDto>> Handle(ListarTurnosQuery request, CancellationToken ct)
    {
        var turnos = await catalogos.ListarTurnosAsync(ct);

        return turnos.Select(t => new CatalogoItemDto(t.IdTurno, t.NombreTurno)).ToList();
    }
}
