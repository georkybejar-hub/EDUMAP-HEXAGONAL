namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.EliminarCanalContacto;

public sealed class EliminarCanalContactoHandler(
    ISedeRepository sedes,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarCanalContactoCommand>
{
    public async Task Handle(EliminarCanalContactoCommand request, CancellationToken ct)
    {
        var canal = await sedes.ObtenerCanalAsync(request.IdContacto, ct)
            ?? throw new NotFoundException("Canal de contacto", request.IdContacto);

        usuarioActual.ValidarGestionInstitucion(canal.IdSedeNavigation.IdInstitucionNavigation);

        sedes.EliminarCanal(canal);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
