namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.ActualizarInstitucion;

public sealed class ActualizarInstitucionHandler(
    IInstitucionRepository instituciones,
    ICatalogoRepository catalogos,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ActualizarInstitucionCommand>
{
    public async Task Handle(ActualizarInstitucionCommand request, CancellationToken ct)
    {
        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        usuarioActual.ValidarGestionInstitucion(institucion);

        if (!await catalogos.ExisteCategoriaInstitucionAsync(request.IdCategoria, ct))
            throw new NotFoundException("CategorÃ­a de instituciÃ³n", request.IdCategoria);

        if (!await catalogos.ExisteTipoGestionAsync(request.IdTipoGestion, ct))
            throw new NotFoundException("Tipo de gestiÃ³n", request.IdTipoGestion);

        if (request.IdReguladora is { } idReguladora && !await catalogos.ExisteEntidadReguladoraAsync(idReguladora, ct))
            throw new NotFoundException("Entidad reguladora", idReguladora);

        if (request.IdCategoria != institucion.IdCategoria &&
            await instituciones.TieneOfertasAsync(institucion.IdInstitucion, ct))
            throw new BusinessRuleException(
                "No se puede cambiar la categorÃ­a porque la instituciÃ³n tiene ofertas educativas registradas.");

        var codigoModular = Normalizar(request.CodigoModular);
        var ruc = Normalizar(request.Ruc);

        if (codigoModular is not null &&
            await instituciones.ExisteCodigoModularAsync(codigoModular, institucion.IdInstitucion, ct))
            throw new ConflictException($"Ya existe otra instituciÃ³n con el cÃ³digo modular {codigoModular}.");

        if (ruc is not null && await instituciones.ExisteRucAsync(ruc, institucion.IdInstitucion, ct))
            throw new ConflictException($"Ya existe otra instituciÃ³n con el RUC {ruc}.");

        if (institucion.CambiaIdentificadores(codigoModular, ruc) && !usuarioActual.EsAdministrador())
            institucion.RevocarVerificacion();

        institucion.IdCategoria = request.IdCategoria;
        institucion.IdTipoGestion = request.IdTipoGestion;
        institucion.IdReguladora = request.IdReguladora;
        institucion.Nombre = request.Nombre.Trim();
        institucion.CodigoModular = codigoModular;
        institucion.Ruc = ruc;
        institucion.Descripcion = request.Descripcion.Trim();
        institucion.SitioWeb = Normalizar(request.SitioWeb);
        institucion.LogoUrl = Normalizar(request.LogoUrl);

        await unitOfWork.SaveChangesAsync(ct);
    }

    private static string? Normalizar(string? valor) =>
        string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
}
