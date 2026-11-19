using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Domain.Models;

namespace EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

public sealed record GetAllInstitutionSearchViewsQuery : IRequest<IReadOnlyList<InstitutionSearchViewDto>>
{
    public string? SearchTerm { get; init; }
    public short? IdCategoria { get; init; }
    public short? IdDistrito { get; init; }
    public short? IdTipoGestion { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

internal sealed class GetAllInstitutionSearchViewsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllInstitutionSearchViewsQuery, IReadOnlyList<InstitutionSearchViewDto>>
{
    public async Task<IReadOnlyList<InstitutionSearchViewDto>> Handle(
        GetAllInstitutionSearchViewsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<VwBuscadorInstitucione>()
            .GetAllAsync(cancellationToken);

        var query = entities.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.Trim();
            query = query.Where(e =>
                (!string.IsNullOrEmpty(e.Nombre) && e.Nombre.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(e.NombreDistrito) && e.NombreDistrito.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(e.NombreCategoria) && e.NombreCategoria.Contains(term, StringComparison.OrdinalIgnoreCase)));
        }

        if (request.IdCategoria.HasValue)
        {
            query = query.Where(e => e.IdCategoria == request.IdCategoria.Value);
        }

        if (request.IdDistrito.HasValue)
        {
            query = query.Where(e => e.IdDistrito == request.IdDistrito.Value);
        }

        if (request.IdTipoGestion.HasValue)
        {
            query = query.Where(e => e.IdTipoGestion == request.IdTipoGestion.Value);
        }

        if (request.Page > 0 && request.PageSize > 0)
        {
            query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);
        }

        return query
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
