using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Districts.Queries;

public sealed record GetAllDistrictsQuery : IRequest<IReadOnlyList<DistrictDto>>;

internal sealed class GetAllDistrictsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllDistrictsQuery, IReadOnlyList<DistrictDto>>
{
    public async Task<IReadOnlyList<DistrictDto>> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Distrito>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new DistrictDto
            {
                IdDistrito = e.IdDistrito,
                NombreDistrito = e.NombreDistrito,
                Ubigeo = e.Ubigeo,
                LatitudCentro = e.LatitudCentro,
                LongitudCentro = e.LongitudCentro
            })
            .ToList();
    }
}

