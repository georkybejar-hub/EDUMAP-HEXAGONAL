namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.CrearInstitucion;

public sealed class CrearInstitucionHandler(
    IInstitucionRepository instituciones,
    ICatalogoRepository catalogos,
    IUsuarioRepository usuarios,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CrearInstitucionCommand, int>
{
    public async Task<int> Handle(CrearInstitucionCommand request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();
        var esAdmin = usuarioActual.EsAdministrador();

        if (!esAdmin && !usuarioActual.TieneRol(RolesSistema.Representante))
            throw new ForbiddenException("Solo los representantes o administradores pueden registrar instituciones.");

        var idRepresentante = await ResolverRepresentanteAsync(request.IdRepresentante, idUsuario, esAdmin, ct);

        await ValidarCatalogosAsync(request, ct);

        var codigoModular = Normalizar(request.CodigoModular);
        var ruc = Normalizar(request.Ruc);

        if (codigoModular is not null && await instituciones.ExisteCodigoModularAsync(codigoModular, null, ct))
            throw new ConflictException($"Ya existe una instituciÃ³n con el cÃ³digo modular {codigoModular}.");

        if (ruc is not null && await instituciones.ExisteRucAsync(ruc, null, ct))
            throw new ConflictException($"Ya existe una instituciÃ³n con el RUC {ruc}.");

        var institucion = new Institucione
        {
            IdCategoria = request.IdCategoria,
            IdTipoGestion = request.IdTipoGestion,
            IdReguladora = request.IdReguladora,
            IdRepresentante = idRepresentante,
            Nombre = request.Nombre.Trim(),
            CodigoModular = codigoModular,
            Ruc = ruc,
            Descripcion = request.Descripcion.Trim(),
            SitioWeb = Normalizar(request.SitioWeb),
            LogoUrl = Normalizar(request.LogoUrl),
            Verificada = false
        };

        instituciones.Agregar(institucion);
        await unitOfWork.SaveChangesAsync(ct);

        return institucion.IdInstitucion;
    }

    private async Task<int?> ResolverRepresentanteAsync(int? solicitado, int idUsuario, bool esAdmin, CancellationToken ct)
    {
        if (!esAdmin)
        {
            if (solicitado is not null && solicitado != idUsuario)
                throw new ForbiddenException("No puedes registrar una instituciÃ³n a nombre de otro representante.");
            return idUsuario;
        }

        if (solicitado is null)
            return null;

        var representante = await usuarios.ObtenerPorIdAsync(solicitado.Value, ct)
            ?? throw new NotFoundException("Usuario representante", solicitado.Value);

        if (!string.Equals(representante.IdRolNavigation.NombreRol, RolesSistema.Representante, StringComparison.OrdinalIgnoreCase))
            throw new BusinessRuleException("El usuario indicado no tiene el rol de representante.");

        if (representante.Activo == false)
            throw new BusinessRuleException("El usuario representante estÃ¡ desactivado.");

        return representante.IdUsuario;
    }

    private async Task ValidarCatalogosAsync(CrearInstitucionCommand request, CancellationToken ct)
    {
        if (!await catalogos.ExisteCategoriaInstitucionAsync(request.IdCategoria, ct))
            throw new NotFoundException("CategorÃ­a de instituciÃ³n", request.IdCategoria);

        if (!await catalogos.ExisteTipoGestionAsync(request.IdTipoGestion, ct))
            throw new NotFoundException("Tipo de gestiÃ³n", request.IdTipoGestion);

        if (request.IdReguladora is { } idReguladora && !await catalogos.ExisteEntidadReguladoraAsync(idReguladora, ct))
            throw new NotFoundException("Entidad reguladora", idReguladora);
    }

    private static string? Normalizar(string? valor) =>
        string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
}
