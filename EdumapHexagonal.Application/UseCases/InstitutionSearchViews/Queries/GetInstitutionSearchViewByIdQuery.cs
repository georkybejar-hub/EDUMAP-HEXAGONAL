using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

public sealed record GetInstitutionSearchViewByIdQuery : IRequest<InstitutionSearchViewDto?>
{
    public int IdInstitucion { get; init; }
}

internal sealed class GetInstitutionSearchViewByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInstitutionSearchViewByIdQuery, InstitutionSearchViewDto?>
{
    public async Task<InstitutionSearchViewDto?> Handle(
        GetInstitutionSearchViewByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.VwBuscadorInstitucione>()
            .FirstOrDefaultAsync(
                e => e.IdInstitucion == request.IdInstitucion,
                cancellationToken);

        if (entity is null) return null;

        return new InstitutionSearchViewDto
        {
            IdInstitucion = entity.IdInstitucion,
            Nombre = entity.Nombre,
            IdCategoria = entity.IdCategoria,
            NombreCategoria = entity.NombreCategoria,
            IdTipoGestion = entity.IdTipoGestion,
            NombreGestion = entity.NombreGestion,
            EnteRegulador = entity.EnteRegulador,
            IdSedePrincipal = entity.IdSedePrincipal,
            IdDistrito = entity.IdDistrito,
            NombreDistrito = entity.NombreDistrito,
            Direccion = entity.Direccion,
            Latitud = entity.Latitud,
            Longitud = entity.Longitud,
            PensionMinima = entity.PensionMinima,
            PensionMaxima = entity.PensionMaxima,
            CalificacionPromedio = entity.CalificacionPromedio,
            TotalResenas = entity.TotalResenas,
            TotalGuardadosComparador = entity.TotalGuardadosComparador
        };
    }
}