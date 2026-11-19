using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

public sealed record CreateRegulatoryEntityCommand : IRequest<RegulatoryEntityDto>
{
    public string Siglas { get; init; } = string.Empty;
    public string NombreEntidad { get; init; } = string.Empty;
}

internal sealed class CreateRegulatoryEntityCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRegulatoryEntityCommand, RegulatoryEntityDto>
{
    public async Task<RegulatoryEntityDto> Handle(CreateRegulatoryEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.EntidadesReguladora
        {
            Siglas = request.Siglas,
            NombreEntidad = request.NombreEntidad
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.EntidadesReguladora>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegulatoryEntityDto
        {
            IdReguladora = entity.IdReguladora,
            Siglas = entity.Siglas,
            NombreEntidad = entity.NombreEntidad
        };
    }
}

