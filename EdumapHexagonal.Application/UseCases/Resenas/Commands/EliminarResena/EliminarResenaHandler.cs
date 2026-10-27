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
            ?? throw new NotFoundException("ReseÃ±a", request.IdResena);

        if (!resena.EsAutor(idUsuario) && !usuarioActual.EsAdministrador())
            throw new ForbiddenException("Solo el autor o un administrador pueden eliminar esta reseÃ±a.");

        resenas.Eliminar(resena);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
