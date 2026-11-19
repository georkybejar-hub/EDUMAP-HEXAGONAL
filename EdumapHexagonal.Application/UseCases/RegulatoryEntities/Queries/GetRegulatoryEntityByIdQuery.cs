using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

public sealed record GetRegulatoryEntityByIdQuery(short Id) : IRequest<RegulatoryEntityDto?>;

internal sealed class GetRegulatoryEntityByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetRegulatoryEntityByIdQuery, RegulatoryEntityDto?>
{
    public async Task<RegulatoryEntityDto?> Handle(GetRegulatoryEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.EntidadesReguladora>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (e is null) return null;

        return new RegulatoryEntityDto
        {
            IdReguladora = e.IdReguladora,
            Siglas = e.Siglas,
            NombreEntidad = e.NombreEntidad
        };
    }
}

