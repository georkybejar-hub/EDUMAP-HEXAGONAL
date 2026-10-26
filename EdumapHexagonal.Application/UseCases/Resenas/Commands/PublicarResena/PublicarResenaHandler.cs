namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.PublicarResena;

public sealed class PublicarResenaHandler(
    IInstitucionRepository instituciones,
    IResenaRepository resenas,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<PublicarResenaCommand, int>
{
    public async Task<int> Handle(PublicarResenaCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("Institución", request.IdInstitucion);

        // Evita que una institución infle su propia calificación.
        if (institucion.EsGestionadaPor(idUsuario))
            throw new BusinessRuleException("No puedes reseñar una institución que tú representas.");

        if (await resenas.ExisteDeUsuarioAsync(request.IdInstitucion, idUsuario, ct))
            throw new ConflictException("Ya publicaste una reseña para esta institución. Puedes editarla.");

        // Las reglas de contenido (puntuación 1-5, longitud del comentario) las aplica el dominio.
        var resena = Resena.Crear(request.IdInstitucion, idUsuario, request.Puntuacion, request.Comentario);

        resenas.Agregar(resena);
        await unitOfWork.SaveChangesAsync(ct);

        return resena.IdResena;
    }
}
