using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Campuses.Queries;

internal sealed class GetAllCampusesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllCampusesQuery, IEnumerable<CampusDto>>
{
    public async Task<IEnumerable<CampusDto>> Handle(GetAllCampusesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().GetAllAsync();
        return entities.Select(e => new CampusDto
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
        });
    }
}
