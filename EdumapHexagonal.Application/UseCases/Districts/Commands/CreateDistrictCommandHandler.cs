using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

internal sealed class CreateDistrictCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateDistrictCommand, DistrictDto>
{
    public async Task<DistrictDto> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.Distrito
        {
            NombreDistrito = request.NombreDistrito,
            Ubigeo = request.Ubigeo,
            LatitudCentro = request.LatitudCentro,
            LongitudCentro = request.LongitudCentro
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
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
