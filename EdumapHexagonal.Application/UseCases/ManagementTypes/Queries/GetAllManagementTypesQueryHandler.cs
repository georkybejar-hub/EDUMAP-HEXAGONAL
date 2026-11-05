using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

internal sealed class GetAllManagementTypesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllManagementTypesQuery, IEnumerable<ManagementTypeDto>>
{
    public async Task<IEnumerable<ManagementTypeDto>> Handle(GetAllManagementTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>().GetAllAsync();
        return entities.Select(e => new ManagementTypeDto
        {
            IdTipoGestion = e.IdTipoGestion,
            NombreGestion = e.NombreGestion
        });
    }
}
