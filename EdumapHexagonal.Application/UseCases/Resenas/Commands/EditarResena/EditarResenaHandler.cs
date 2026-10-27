namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.EditarResena;

public sealed class EditarResenaHandler(
    IResenaRepository resenas,
    ICurrentUserService usuarioActual,
    IDateTimeProvider reloj,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EditarResenaCommand>
{
    public async Task Handle(EditarResenaCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var resena = await resenas.ObtenerPorIdAsync(request.IdResena, ct)
            ?? throw new NotFoundException("ReseÃ±a", request.IdResena);

        if (!resena.EsAutor(idUsuario))
            throw new ForbiddenException("Solo el autor puede editar esta reseÃ±a.");

        resena.Editar(request.Puntuacion, request.Comentario, reloj.Ahora);

        await unitOfWork.SaveChangesAsync(ct);
    }
}
