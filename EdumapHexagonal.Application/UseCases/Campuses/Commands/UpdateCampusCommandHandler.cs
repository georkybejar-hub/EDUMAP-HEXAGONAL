using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

internal sealed class UpdateCampusCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateCampusCommand, CampusDto?>
{
    public async Task<CampusDto?> Handle(UpdateCampusCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().GetByIdAsync(request.IdSede);
        if (entity == null) return null;

        entity.IdInstitucion = request.IdInstitucion;
        entity.IdDistrito = request.IdDistrito;
        entity.NombreSede = request.NombreSede;
        entity.Direccion = request.Direccion;
        entity.Referencia = request.Referencia;
        entity.Latitud = request.Latitud;
        entity.Longitud = request.Longitud;
        entity.EsPrincipal = request.EsPrincipal;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
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
