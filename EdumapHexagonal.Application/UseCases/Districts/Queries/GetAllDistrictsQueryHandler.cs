using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Districts.Queries;

internal sealed class GetAllDistrictsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllDistrictsQuery, IEnumerable<DistrictDto>>
{
    public async Task<IEnumerable<DistrictDto>> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>().GetAllAsync();
        return entities.Select(e => new DistrictDto
        {
            IdDistrito = e.IdDistrito,
            NombreDistrito = e.NombreDistrito,
            Ubigeo = e.Ubigeo,
            LatitudCentro = e.LatitudCentro,
            LongitudCentro = e.LongitudCentro
        });
    }
}
