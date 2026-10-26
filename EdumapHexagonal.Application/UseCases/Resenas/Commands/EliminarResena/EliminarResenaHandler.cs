namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.EliminarResena;

public sealed class EliminarResenaHandler(
    IResenaRepository resenas,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarResenaCommand>
{
    public async Task Handle(EliminarResenaCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var resena = await resenas.ObtenerPorIdAsync(request.IdResena, ct)
            ?? throw new NotFoundException("Reseña", request.IdResena);

        if (!resena.EsAutor(idUsuario) && !usuarioActual.EsAdministrador())
            throw new ForbiddenException("Solo el autor o un administrador pueden eliminar esta reseña.");

        // Los reportes de moderación de la reseña se eliminan en cascada (FK en la base de datos).
        resenas.Eliminar(resena);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
