namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.RegistrarSede;

public sealed class RegistrarSedeHandler(
    IInstitucionRepository instituciones,
    ISedeRepository sedes,
    ICatalogoRepository catalogos,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegistrarSedeCommand, int>
{
    public async Task<int> Handle(RegistrarSedeCommand request, CancellationToken ct)
    {
        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("Institución", request.IdInstitucion);

        usuarioActual.ValidarGestionInstitucion(institucion);

        if (!await catalogos.ExisteDistritoAsync(request.IdDistrito, ct))
            throw new NotFoundException("Distrito", request.IdDistrito);

        var sedesExistentes = await sedes.ContarPorInstitucionAsync(request.IdInstitucion, ct);
        var seraPrincipal = sedesExistentes == 0 || request.EsPrincipal;

        // Solo puede haber una sede principal por institución.
        if (seraPrincipal && sedesExistentes > 0)
        {
            var principalActual = await sedes.ObtenerPrincipalAsync(request.IdInstitucion, ct);
            principalActual?.DesmarcarComoPrincipal();
        }

        var sede = new SedesInstitucion
        {
            IdInstitucion = request.IdInstitucion,
            NombreSede = request.NombreSede.Trim(),
            EsPrincipal = seraPrincipal
        };
        sede.CambiarUbicacion(request.IdDistrito, request.Direccion, request.Referencia, request.Latitud, request.Longitud);

        sedes.Agregar(sede);
        await unitOfWork.SaveChangesAsync(ct);

        return sede.IdSede;
    }
}
