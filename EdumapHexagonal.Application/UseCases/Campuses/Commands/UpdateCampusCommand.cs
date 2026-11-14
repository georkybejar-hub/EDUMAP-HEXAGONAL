using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

public sealed record UpdateCampusCommand : IRequest<CampusDto?>
{
    public int IdSede { get; init; }
    public int IdInstitucion { get; init; }
    public short IdDistrito { get; init; }
    public string NombreSede { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public string? Referencia { get; init; }
    public decimal Latitud { get; init; }
    public decimal Longitud { get; init; }
    public bool? EsPrincipal { get; init; }
}

internal sealed class UpdateCampusCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCampusCommand, CampusDto?>
{
    public async Task<CampusDto?> Handle(UpdateCampusCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>();

        var entity = await repository.GetByIdAsync(request.IdSede, cancellationToken);
        if (entity is null) return null;

        entity.IdInstitucion = request.IdInstitucion;
        entity.IdDistrito = request.IdDistrito;
        entity.NombreSede = request.NombreSede;
        entity.Direccion = request.Direccion;
        entity.Referencia = request.Referencia;
        entity.Latitud = request.Latitud;
        entity.Longitud = request.Longitud;
        entity.EsPrincipal = request.EsPrincipal;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

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
