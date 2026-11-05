using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

internal sealed class UpdateDistrictCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateDistrictCommand, DistrictDto?>
{
    public async Task<DistrictDto?> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>().GetByIdAsync(request.IdDistrito);
        if (entity == null) return null;

        entity.NombreDistrito = request.NombreDistrito;
        entity.Ubigeo = request.Ubigeo;
        entity.LatitudCentro = request.LatitudCentro;
        entity.LongitudCentro = request.LongitudCentro;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>().Update(entity);
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
