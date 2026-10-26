namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.AgregarCanalContacto;

public sealed class AgregarCanalContactoHandler(
    ISedeRepository sedes,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AgregarCanalContactoCommand, int>
{
    public async Task<int> Handle(AgregarCanalContactoCommand request, CancellationToken ct)
    {
        var sede = await sedes.ObtenerPorIdAsync(request.IdSede, ct)
            ?? throw new NotFoundException("Sede", request.IdSede);

        usuarioActual.ValidarGestionInstitucion(sede.IdInstitucionNavigation);

        var tipo = TiposCanal.Normalizar(request.TipoCanal);
        var valor = request.ValorContacto.Trim();

        if (await sedes.ExisteCanalAsync(request.IdSede, tipo, valor, ct))
            throw new ConflictException("La sede ya tiene registrado ese canal de contacto.");

        var canal = new CanalesContactoSede
        {
            IdSede = request.IdSede,
            TipoCanal = tipo,
            ValorContacto = valor,
            AreaAtencion = string.IsNullOrWhiteSpace(request.AreaAtencion)
                ? SedesInstitucion.AreaAtencionPorDefecto
                : request.AreaAtencion.Trim(),
            HorarioAtencion = string.IsNullOrWhiteSpace(request.HorarioAtencion) ? null : request.HorarioAtencion.Trim()
        };

        sedes.AgregarCanal(canal);
        await unitOfWork.SaveChangesAsync(ct);

        return canal.IdContacto;
    }
}
