using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

public sealed record GetAllRegulatoryEntitiesQuery : IRequest<IReadOnlyList<RegulatoryEntityDto>>;

internal sealed class GetAllRegulatoryEntitiesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllRegulatoryEntitiesQuery, IReadOnlyList<RegulatoryEntityDto>>
{
    public async Task<IReadOnlyList<RegulatoryEntityDto>> Handle(
        GetAllRegulatoryEntitiesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new RegulatoryEntityDto
            {
                IdReguladora = e.IdReguladora,
                Siglas = e.Siglas,
                NombreEntidad = e.NombreEntidad
            })
            .ToList();
    }
}
