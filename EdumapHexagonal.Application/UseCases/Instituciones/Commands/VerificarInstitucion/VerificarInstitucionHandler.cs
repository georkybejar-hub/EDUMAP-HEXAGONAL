namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.VerificarInstitucion;

public sealed class VerificarInstitucionHandler(
    IInstitucionRepository instituciones,
    ISedeRepository sedes,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<VerificarInstitucionCommand>
{
    public async Task Handle(VerificarInstitucionCommand request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        if (!request.Verificada)
        {
            institucion.RevocarVerificacion();
            await unitOfWork.SaveChangesAsync(ct);
            return;
        }

        if (institucion.EstaVerificada())
            return;

        if (string.IsNullOrWhiteSpace(institucion.CodigoModular) && string.IsNullOrWhiteSpace(institucion.Ruc))
            throw new BusinessRuleException("La instituciÃ³n debe tener cÃ³digo modular o RUC para ser verificada.");

        if (await sedes.ContarPorInstitucionAsync(institucion.IdInstitucion, ct) == 0)
            throw new BusinessRuleException("La instituciÃ³n debe tener al menos una sede registrada para ser verificada.");

        institucion.Verificar();
        await unitOfWork.SaveChangesAsync(ct);
    }
}
