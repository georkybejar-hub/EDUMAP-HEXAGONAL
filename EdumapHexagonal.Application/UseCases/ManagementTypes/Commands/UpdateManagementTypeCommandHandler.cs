using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

internal sealed class UpdateManagementTypeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateManagementTypeCommand, ManagementTypeDto?>
{
    public async Task<ManagementTypeDto?> Handle(UpdateManagementTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>().GetByIdAsync(request.IdTipoGestion);
        if (entity == null) return null;

        entity.NombreGestion = request.NombreGestion;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new ManagementTypeDto
        {
            IdTipoGestion = e.IdTipoGestion,
            NombreGestion = e.NombreGestion
        };
    }
}
