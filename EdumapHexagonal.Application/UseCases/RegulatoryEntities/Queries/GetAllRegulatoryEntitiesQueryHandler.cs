using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

internal sealed class GetAllRegulatoryEntitiesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllRegulatoryEntitiesQuery, IEnumerable<RegulatoryEntityDto>>
{
    public async Task<IEnumerable<RegulatoryEntityDto>> Handle(GetAllRegulatoryEntitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>().GetAllAsync();
        return entities.Select(e => new RegulatoryEntityDto
        {
            IdReguladora = e.IdReguladora,
            Siglas = e.Siglas,
            NombreEntidad = e.NombreEntidad
        });
    }
}
