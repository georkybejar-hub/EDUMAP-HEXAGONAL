using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Campuses.Queries;

internal sealed class GetCampusByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetCampusByIdQuery, CampusDto?>
{
    public async Task<CampusDto?> Handle(GetCampusByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new CampusDto
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
        };
    }
}
