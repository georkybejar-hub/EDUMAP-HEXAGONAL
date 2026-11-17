using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

public sealed record CreateManagementTypeCommand : IRequest<ManagementTypeDto>
{
    public string NombreGestion { get; init; } = string.Empty;
}

internal sealed class CreateManagementTypeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateManagementTypeCommand, ManagementTypeDto>
{
    public async Task<ManagementTypeDto> Handle(
        CreateManagementTypeCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.TiposGestion
        {
            NombreGestion = request.NombreGestion
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TiposGestion>()
            .AddAsync(entity, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ManagementTypeDto
        {
            IdTipoGestion = entity.IdTipoGestion,
            NombreGestion = entity.NombreGestion
        };
    }
}