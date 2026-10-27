namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.ActualizarSede;

public sealed class ActualizarSedeHandler(
    ISedeRepository sedes,
    ICatalogoRepository catalogos,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ActualizarSedeCommand>
{
    public async Task Handle(ActualizarSedeCommand request, CancellationToken ct)
    {
        var sede = await sedes.ObtenerPorIdAsync(request.IdSede, ct)
            ?? throw new NotFoundException("Sede", request.IdSede);

        usuarioActual.ValidarGestionInstitucion(sede.IdInstitucionNavigation);

        if (!await catalogos.ExisteDistritoAsync(request.IdDistrito, ct))
            throw new NotFoundException("Distrito", request.IdDistrito);

        if (request.EsPrincipal && !sede.EsSedePrincipal())
        {
            var principalActual = await sedes.ObtenerPrincipalAsync(sede.IdInstitucion, ct);
            principalActual?.DesmarcarComoPrincipal();
            sede.MarcarComoPrincipal();
        }
        else if (!request.EsPrincipal && sede.EsSedePrincipal())
        {
            throw new BusinessRuleException(
                "La instituciÃ³n debe tener una sede principal. Marca otra sede como principal en lugar de desmarcar esta.");
        }

        sede.NombreSede = request.NombreSede.Trim();
        sede.CambiarUbicacion(request.IdDistrito, request.Direccion, request.Referencia, request.Latitud, request.Longitud);

        await unitOfWork.SaveChangesAsync(ct);
    }
}
