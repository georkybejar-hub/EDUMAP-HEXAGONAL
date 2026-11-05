using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

internal sealed class CreateManagementTypeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateManagementTypeCommand, ManagementTypeDto>
{
    public async Task<ManagementTypeDto> Handle(CreateManagementTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.TiposGestion
        {
            NombreGestion = request.NombreGestion
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new ManagementTypeDto
        {
            IdTipoGestion = e.IdTipoGestion,
            NombreGestion = e.NombreGestion
        };
    }
}
