using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

internal sealed class GetAllInstitutionSearchViewsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllInstitutionSearchViewsQuery, IEnumerable<InstitutionSearchViewDto>>
{
    public async Task<IEnumerable<InstitutionSearchViewDto>> Handle(GetAllInstitutionSearchViewsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.VwBuscadorInstitucione>().GetAllAsync();
        return entities.Select(e => new InstitutionSearchViewDto
        {
            IdInstitucion = e.IdInstitucion,
            Nombre = e.Nombre,
            IdCategoria = e.IdCategoria,
            NombreCategoria = e.NombreCategoria,
            IdTipoGestion = e.IdTipoGestion,
            NombreGestion = e.NombreGestion,
            EnteRegulador = e.EnteRegulador,
            IdSedePrincipal = e.IdSedePrincipal,
            IdDistrito = e.IdDistrito,
            NombreDistrito = e.NombreDistrito,
            Direccion = e.Direccion,
            Latitud = e.Latitud,
            Longitud = e.Longitud,
            PensionMinima = e.PensionMinima,
            PensionMaxima = e.PensionMaxima,
            CalificacionPromedio = e.CalificacionPromedio,
            TotalResenas = e.TotalResenas,
            TotalGuardadosComparador = e.TotalGuardadosComparador
        });
    }
}
