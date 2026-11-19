using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

public sealed record UpdateRegulatoryEntityCommand : IRequest<RegulatoryEntityDto?>
{
    public short IdReguladora { get; init; }
    public string Siglas { get; init; } = string.Empty;
    public string NombreEntidad { get; init; } = string.Empty;
}

internal sealed class UpdateRegulatoryEntityCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateRegulatoryEntityCommand, RegulatoryEntityDto?>
{
    public async Task<RegulatoryEntityDto?> Handle(UpdateRegulatoryEntityCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.EntidadesReguladora>();

        var entity = await repository.GetByIdAsync(request.IdReguladora, cancellationToken);
        if (entity is null) return null;

        entity.Siglas = request.Siglas;
        entity.NombreEntidad = request.NombreEntidad;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegulatoryEntityDto
        {
            IdReguladora = entity.IdReguladora,
            Siglas = entity.Siglas,
            NombreEntidad = entity.NombreEntidad
        };
    }
}

