using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

internal sealed class CreateCampusCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCampusCommand, CampusDto>
{
    public async Task<CampusDto> Handle(CreateCampusCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.SedesInstitucion
        {
            IdInstitucion = request.IdInstitucion,
            IdDistrito = request.IdDistrito,
            NombreSede = request.NombreSede,
            Direccion = request.Direccion,
            Referencia = request.Referencia,
            Latitud = request.Latitud,
            Longitud = request.Longitud,
            EsPrincipal = request.EsPrincipal
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().AddAsync(entity);
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
