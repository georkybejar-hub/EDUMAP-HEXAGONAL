using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

public sealed record UpdateManagementTypeCommand : IRequest<ManagementTypeDto?>
{
    public short IdTipoGestion { get; init; }
    public string NombreGestion { get; init; } = string.Empty;
}

internal sealed class UpdateManagementTypeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateManagementTypeCommand, ManagementTypeDto?>
{
    public async Task<ManagementTypeDto?> Handle(
        UpdateManagementTypeCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.TiposGestion>();

        var entity = await repository.GetByIdAsync(
            request.IdTipoGestion,
            cancellationToken);

        if (entity is null) return null;

        entity.NombreGestion = request.NombreGestion;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ManagementTypeDto
        {
            IdTipoGestion = entity.IdTipoGestion,
            NombreGestion = entity.NombreGestion
        };
    }
}
