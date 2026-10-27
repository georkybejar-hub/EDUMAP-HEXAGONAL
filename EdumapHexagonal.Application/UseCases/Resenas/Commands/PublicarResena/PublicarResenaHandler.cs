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
            ?? throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        if (institucion.EsGestionadaPor(idUsuario))
            throw new BusinessRuleException("No puedes reseÃ±ar una instituciÃ³n que tÃº representas.");

        if (await resenas.ExisteDeUsuarioAsync(request.IdInstitucion, idUsuario, ct))
            throw new ConflictException("Ya publicaste una reseÃ±a para esta instituciÃ³n. Puedes editarla.");

        var resena = Resena.Crear(request.IdInstitucion, idUsuario, request.Puntuacion, request.Comentario);

        resenas.Agregar(resena);
        await unitOfWork.SaveChangesAsync(ct);

        return resena.IdResena;
    }
}
