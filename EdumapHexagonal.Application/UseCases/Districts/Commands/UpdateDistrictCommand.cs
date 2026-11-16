using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

public sealed record UpdateDistrictCommand : IRequest<DistrictDto?>
{
    public short IdDistrito { get; init; }
    public string NombreDistrito { get; init; } = string.Empty;
    public string Ubigeo { get; init; } = string.Empty;
    public decimal LatitudCentro { get; init; }
    public decimal LongitudCentro { get; init; }
}

internal sealed class UpdateDistrictCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDistrictCommand, DistrictDto?>
{
    public async Task<DistrictDto?> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Distrito>();

        var entity = await repository.GetByIdAsync(request.IdDistrito, cancellationToken);
        if (entity is null) return null;

        entity.NombreDistrito = request.NombreDistrito;
        entity.Ubigeo = request.Ubigeo;
        entity.LatitudCentro = request.LatitudCentro;
        entity.LongitudCentro = request.LongitudCentro;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DistrictDto
        {
            IdDistrito = entity.IdDistrito,
            NombreDistrito = entity.NombreDistrito,
            Ubigeo = entity.Ubigeo,
            LatitudCentro = entity.LatitudCentro,
            LongitudCentro = entity.LongitudCentro
        };
    }
}
