namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.EliminarSede;

public sealed class EliminarSedeHandler(
    ISedeRepository sedes,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarSedeCommand>
{
    public async Task Handle(EliminarSedeCommand request, CancellationToken ct)
    {
        var sede = await sedes.ObtenerPorIdAsync(request.IdSede, ct)
            ?? throw new NotFoundException("Sede", request.IdSede);

        var institucion = sede.IdInstitucionNavigation;
        usuarioActual.ValidarGestionInstitucion(institucion);

        if (await sedes.TieneOfertasAsync(sede.IdSede, ct))
            throw new BusinessRuleException("No se puede eliminar la sede porque tiene ofertas educativas. Elimínalas primero.");

        var totalSedes = await sedes.ContarPorInstitucionAsync(sede.IdInstitucion, ct);

        if (totalSedes == 1 && institucion.EstaVerificada())
            throw new BusinessRuleException("Una institución verificada debe tener al menos una sede.");

        if (sede.EsSedePrincipal() && totalSedes > 1)
            throw new BusinessRuleException("No se puede eliminar la sede principal. Marca otra sede como principal primero.");

        // Los canales de contacto de la sede se eliminan en cascada (FK en la base de datos).
        sedes.Eliminar(sede);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
