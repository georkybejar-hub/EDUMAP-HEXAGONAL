using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Districts.Queries;

public sealed record GetDistrictByIdQuery : IRequest<DistrictDto?>
{
    public short IdDistrito { get; init; }
}

internal sealed class GetDistrictByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDistrictByIdQuery, DistrictDto?>
{
    public async Task<DistrictDto?> Handle(GetDistrictByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Distrito>()
            .GetByIdAsync(request.IdDistrito, cancellationToken);

        if (entity is null) return null;

        return new DistrictDto
        {
            IdDistrito = entity.IdDistrito,
            NombreDistrito = entity.NombreDistrito,
            Ubigeo = entity.Ubigeo,
            LatitudCentro = entity.LatitudCentro,
            LongitudCentro = entity.LongitudCentro
        };
    }
}

