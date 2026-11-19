using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

public sealed record GetAllManagementTypesQuery : IRequest<IReadOnlyList<ManagementTypeDto>>;

internal sealed class GetAllManagementTypesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllManagementTypesQuery, IReadOnlyList<ManagementTypeDto>>
{
    public async Task<IReadOnlyList<ManagementTypeDto>> Handle(
        GetAllManagementTypesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.TiposGestion>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new ManagementTypeDto
            {
                IdTipoGestion = e.IdTipoGestion,
                NombreGestion = e.NombreGestion
            })
            .ToList();
    }
}
