using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Campuses.Queries;

public sealed record GetCampusByIdQuery : IRequest<CampusDto?>
{
    public int IdSede { get; init; }
}

internal sealed class GetCampusByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCampusByIdQuery, CampusDto?>
{
    public async Task<CampusDto?> Handle(GetCampusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>()
            .GetByIdAsync(request.IdSede, cancellationToken);

        if (entity is null) return null;

        return new CampusDto
        {
            IdSede = entity.IdSede,
            IdInstitucion = entity.IdInstitucion,
            IdDistrito = entity.IdDistrito,
            NombreSede = entity.NombreSede,
            Direccion = entity.Direccion,
            Referencia = entity.Referencia,
            Latitud = entity.Latitud,
            Longitud = entity.Longitud,
            EsPrincipal = entity.EsPrincipal
        };
    }
}
