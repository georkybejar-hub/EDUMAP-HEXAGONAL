using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Institutions.Queries;

internal sealed class GetAllInstitutionsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllInstitutionsQuery, IEnumerable<InstitutionDto>>
{
    public async Task<IEnumerable<InstitutionDto>> Handle(GetAllInstitutionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>().GetAllAsync();
        return entities.Select(e => new InstitutionDto
        {
            IdInstitucion = e.IdInstitucion,
            IdCategoria = e.IdCategoria,
            IdTipoGestion = e.IdTipoGestion,
            IdReguladora = e.IdReguladora,
            IdRepresentante = e.IdRepresentante,
            Nombre = e.Nombre,
            CodigoModular = e.CodigoModular,
            Ruc = e.Ruc,
            Descripcion = e.Descripcion,
            SitioWeb = e.SitioWeb,
            LogoUrl = e.LogoUrl,
            Verificada = e.Verificada,
            FechaCreacion = e.FechaCreacion
        });
    }
}
