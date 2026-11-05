using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Districts.Queries;

internal sealed class GetDistrictByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetDistrictByIdQuery, DistrictDto?>
{
    public async Task<DistrictDto?> Handle(GetDistrictByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new DistrictDto
        {
            IdDistrito = e.IdDistrito,
            NombreDistrito = e.NombreDistrito,
            Ubigeo = e.Ubigeo,
            LatitudCentro = e.LatitudCentro,
            LongitudCentro = e.LongitudCentro
        };
    }
}
