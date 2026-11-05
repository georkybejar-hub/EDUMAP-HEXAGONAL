using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Institutions.Queries;

internal sealed class GetInstitutionByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetInstitutionByIdQuery, InstitutionDto?>
{
    public async Task<InstitutionDto?> Handle(GetInstitutionByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new InstitutionDto
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
        };
    }
}
