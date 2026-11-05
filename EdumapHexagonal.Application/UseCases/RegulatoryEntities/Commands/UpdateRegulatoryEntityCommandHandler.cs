using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

internal sealed class UpdateRegulatoryEntityCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateRegulatoryEntityCommand, RegulatoryEntityDto?>
{
    public async Task<RegulatoryEntityDto?> Handle(UpdateRegulatoryEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>().GetByIdAsync(request.IdReguladora);
        if (entity == null) return null;

        entity.Siglas = request.Siglas;
        entity.NombreEntidad = request.NombreEntidad;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new RegulatoryEntityDto
        {
            IdReguladora = e.IdReguladora,
            Siglas = e.Siglas,
            NombreEntidad = e.NombreEntidad
        };
    }
}
