using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Campuses.Queries;

public sealed record GetAllCampusesQuery : IRequest<IReadOnlyList<CampusDto>>;

internal sealed class GetAllCampusesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllCampusesQuery, IReadOnlyList<CampusDto>>
{
    public async Task<IReadOnlyList<CampusDto>> Handle(GetAllCampusesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new CampusDto
            {
                IdSede = e.IdSede,
                IdInstitucion = e.IdInstitucion,
                IdDistrito = e.IdDistrito,
                NombreSede = e.NombreSede,
                Direccion = e.Direccion,
                Referencia = e.Referencia,
                Latitud = e.Latitud,
                Longitud = e.Longitud,
                EsPrincipal = e.EsPrincipal
            })
            .ToList();
    }
}
