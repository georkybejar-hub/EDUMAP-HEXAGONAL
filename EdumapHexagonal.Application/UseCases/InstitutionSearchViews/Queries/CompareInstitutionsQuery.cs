using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Domain.Models;

namespace EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

public sealed record CompareInstitutionsQuery : IRequest<IReadOnlyList<InstitutionSearchViewDto>>
{
    public IReadOnlyList<int> InstitutionIds { get; init; } = Array.Empty<int>();
}

internal sealed class CompareInstitutionsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CompareInstitutionsQuery, IReadOnlyList<InstitutionSearchViewDto>>
{
    public async Task<IReadOnlyList<InstitutionSearchViewDto>> Handle(
        CompareInstitutionsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.InstitutionIds == null || request.InstitutionIds.Count < 2)
        {
            throw new ArgumentException("Debe seleccionar al menos 2 instituciones para comparar.");
        }

        var distinctIds = request.InstitutionIds.Distinct().ToList();

        var entities = await unitOfWork
            .Repository<VwBuscadorInstitucione>()
            .GetAllAsync(e => e.IdInstitucion.HasValue && distinctIds.Contains(e.IdInstitucion.Value), cancellationToken);

        if (entities.Count != distinctIds.Count)
        {
            throw new KeyNotFoundException("Una o más instituciones seleccionadas no fueron encontradas.");
        }

        // Regla de Negocio: Validar que todas las instituciones pertenezcan a la misma categoría educativa
        var categories = entities.Select(e => e.IdCategoria).Distinct().ToList();
        if (categories.Count > 1)
        {
            var categoryNames = string.Join(", ", entities.Select(e => e.NombreCategoria).Distinct());
            throw new InvalidOperationException(
                $"Solo se pueden comparar instituciones de la misma categoría educativa. Categorías seleccionadas: {categoryNames}.");
        }

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
