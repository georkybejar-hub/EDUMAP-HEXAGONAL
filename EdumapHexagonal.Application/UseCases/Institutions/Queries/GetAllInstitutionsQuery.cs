using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Institutions.Queries;

public sealed record GetAllInstitutionsQuery : IRequest<IReadOnlyList<InstitutionDto>>;

internal sealed class GetAllInstitutionsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllInstitutionsQuery, IReadOnlyList<InstitutionDto>>
{
    public async Task<IReadOnlyList<InstitutionDto>> Handle(
        GetAllInstitutionsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Institucione>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new InstitutionDto
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
            })
            .ToList();
    }
}
