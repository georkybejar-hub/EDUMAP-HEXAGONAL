using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

public sealed record GetManagementTypeByIdQuery : IRequest<ManagementTypeDto?>
{
    public short IdTipoGestion { get; init; }
}

internal sealed class GetManagementTypeByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetManagementTypeByIdQuery, ManagementTypeDto?>
{
    public async Task<ManagementTypeDto?> Handle(
        GetManagementTypeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.TiposGestion>()
            .GetByIdAsync(request.IdTipoGestion, cancellationToken);

        if (entity is null) return null;

        return new ManagementTypeDto
        {
            IdTipoGestion = entity.IdTipoGestion,
            NombreGestion = entity.NombreGestion
        };
    }
}