using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

internal sealed class GetManagementTypeByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetManagementTypeByIdQuery, ManagementTypeDto?>
{
    public async Task<ManagementTypeDto?> Handle(GetManagementTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new ManagementTypeDto
        {
            IdTipoGestion = e.IdTipoGestion,
            NombreGestion = e.NombreGestion
        };
    }
}
