using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

internal sealed class CreateRegulatoryEntityCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateRegulatoryEntityCommand, RegulatoryEntityDto>
{
    public async Task<RegulatoryEntityDto> Handle(CreateRegulatoryEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.EntidadesReguladora
        {
            Siglas = request.Siglas,
            NombreEntidad = request.NombreEntidad
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.EntidadesReguladora>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new RegulatoryEntityDto
        {
            IdReguladora = e.IdReguladora,
            Siglas = e.Siglas,
            NombreEntidad = e.NombreEntidad
        };
    }
}
