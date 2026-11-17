using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

public sealed record GetAllInstitutionSearchViewsQuery
    : IRequest<IReadOnlyList<InstitutionSearchViewDto>>;

internal sealed class GetAllInstitutionSearchViewsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllInstitutionSearchViewsQuery, IReadOnlyList<InstitutionSearchViewDto>>
{
    public async Task<IReadOnlyList<InstitutionSearchViewDto>> Handle(
        GetAllInstitutionSearchViewsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.VwBuscadorInstitucione>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new InstitutionSearchViewDto
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
            })
            .ToList();
    }
}