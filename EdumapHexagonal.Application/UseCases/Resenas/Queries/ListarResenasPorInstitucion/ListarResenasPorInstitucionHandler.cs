namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarResenasPorInstitucion;

public sealed class ListarResenasPorInstitucionHandler(
    IInstitucionRepository instituciones,
    IResenaRepository resenas)
    : IRequestHandler<ListarResenasPorInstitucionQuery, ResenasInstitucionDto>
{
    public async Task<ResenasInstitucionDto> Handle(ListarResenasPorInstitucionQuery request, CancellationToken ct)
    {
        if (!await instituciones.ExisteAsync(request.IdInstitucion, ct))
            throw new NotFoundException("Institución", request.IdInstitucion);

        var conteoPorPuntuacion = await resenas.ObtenerDistribucionPuntuacionAsync(request.IdInstitucion, ct);

        // Se devuelven siempre las 5 estrellas, aunque alguna tenga 0 reseñas.
        var distribucion = Enumerable
            .Range(Resena.PuntuacionMinima, Resena.PuntuacionMaxima)
            .Select(p => (short)p)
            .ToDictionary(p => p, p => conteoPorPuntuacion.GetValueOrDefault(p));

        var total = distribucion.Values.Sum();
        var promedio = total == 0
            ? 0m
            : Math.Round((decimal)distribucion.Sum(d => d.Key * d.Value) / total, 1);

        var pagina = await resenas.ListarVisiblesPorInstitucionAsync(
            request.IdInstitucion, request.Pagina, request.TamanoPagina, ct);

        return new ResenasInstitucionDto(
            request.IdInstitucion,
            promedio,
            total,
            distribucion,
            pagina.Map(r => new ResenaDto(
                r.IdResena,
                NombrePublico(r.IdUsuarioNavigation),
                r.Puntuacion,
                r.Comentario,
                r.FechaPublicacion,
                r.FechaActualizacion is not null)));
    }

    /// <summary>Por privacidad solo se muestra el nombre y la inicial del apellido: "María G."</summary>
    private static string NombrePublico(Usuario usuario)
    {
        var nombre = usuario.Nombres.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "Usuario";
        var inicial = usuario.Apellidos.Trim().FirstOrDefault();
        return inicial == default ? nombre : $"{nombre} {char.ToUpperInvariant(inicial)}.";
    }
}
