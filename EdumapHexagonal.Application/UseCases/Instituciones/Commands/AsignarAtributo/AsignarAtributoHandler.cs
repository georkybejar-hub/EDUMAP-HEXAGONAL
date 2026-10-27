namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.AsignarAtributo;

public sealed class AsignarAtributoHandler(
    IInstitucionRepository instituciones,
    ICatalogoRepository catalogos,
    ICurrentUserService usuarioActual,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AsignarAtributoCommand>
{
    public async Task Handle(AsignarAtributoCommand request, CancellationToken ct)
    {
        var institucion = await instituciones.ObtenerPorIdAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        usuarioActual.ValidarGestionInstitucion(institucion);

        if (!await catalogos.ExisteAtributoAsync(request.IdAtributo, ct))
            throw new NotFoundException("Atributo", request.IdAtributo);

        var detalle = string.IsNullOrWhiteSpace(request.DetalleValor) ? null : request.DetalleValor.Trim();
        var existente = await instituciones.ObtenerAtributoAsync(request.IdInstitucion, request.IdAtributo, ct);

        if (existente is not null)
        {
            existente.DetalleValor = detalle;
        }
        else
        {
            instituciones.AgregarAtributo(new InstitucionAtributo
            {
                IdInstitucion = request.IdInstitucion,
                IdAtributo = request.IdAtributo,
                DetalleValor = detalle
            });
        }

        await unitOfWork.SaveChangesAsync(ct);
    }
}
