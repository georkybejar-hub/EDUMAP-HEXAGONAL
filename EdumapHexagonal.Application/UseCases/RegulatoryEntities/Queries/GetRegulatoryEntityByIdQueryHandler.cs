using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

internal sealed class GetRegulatoryEntityByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetRegulatoryEntityByIdQuery, RegulatoryEntityDto?>
{
    public async Task<RegulatoryEntityDto?> Handle(GetRegulatoryEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new RegulatoryEntityDto
        {
            IdReguladora = e.IdReguladora,
            Siglas = e.Siglas,
            NombreEntidad = e.NombreEntidad
        };
    }
}
