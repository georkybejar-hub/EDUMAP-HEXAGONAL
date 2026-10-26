namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarMisResenas;

public sealed class ListarMisResenasHandler(
    IResenaRepository resenas,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ListarMisResenasQuery, IReadOnlyList<MiResenaDto>>
{
    public async Task<IReadOnlyList<MiResenaDto>> Handle(ListarMisResenasQuery request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var lista = await resenas.ListarPorUsuarioAsync(idUsuario, ct);

        return lista
            .Select(r => new MiResenaDto(
                r.IdResena,
                r.IdInstitucion,
                r.IdInstitucionNavigation.Nombre,
                r.Puntuacion,
                r.Comentario,
                r.EstaVisible(),
                r.FechaPublicacion,
                r.FechaActualizacion))
            .ToList();
    }
}
