using EdumapHexagonal.Application.Mapping;
using EdumapHexagonal.Application.UseCases.Sedes.Queries.ListarSedesPorInstitucion;

namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ObtenerSedePorId;

public sealed class ObtenerSedePorIdHandler(ISedeRepository sedes)
    : IRequestHandler<ObtenerSedePorIdQuery, SedeDto>
{
    public async Task<SedeDto> Handle(ObtenerSedePorIdQuery request, CancellationToken ct)
    {
        var sede = await sedes.ObtenerPorIdAsync(request.IdSede, ct)
            ?? throw new NotFoundException("Sede", request.IdSede);

        return sede.ToDto();
    }
}
