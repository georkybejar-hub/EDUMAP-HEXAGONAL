using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

public sealed record CreateCampusCommand : IRequest<CampusDto>
{
    public int IdInstitucion { get; init; }
    public short IdDistrito { get; init; }
    public string NombreSede { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public string? Referencia { get; init; }
    public decimal Latitud { get; init; }
    public decimal Longitud { get; init; }
    public bool? EsPrincipal { get; init; }
}

internal sealed class CreateCampusCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCampusCommand, CampusDto>
{
    public async Task<CampusDto> Handle(CreateCampusCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.SedesInstitucion
        {
            IdInstitucion = request.IdInstitucion,
            IdDistrito = request.IdDistrito,
            NombreSede = request.NombreSede,
            Direccion = request.Direccion,
            Referencia = request.Referencia,
            Latitud = request.Latitud,
            Longitud = request.Longitud,
            EsPrincipal = request.EsPrincipal
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.SedesInstitucion>().AddAsync(entity, cancellationToken);
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
