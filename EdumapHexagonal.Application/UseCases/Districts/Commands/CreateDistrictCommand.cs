using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

public sealed record CreateDistrictCommand : IRequest<DistrictDto>
{
    public string NombreDistrito { get; init; } = string.Empty;
    public string Ubigeo { get; init; } = string.Empty;
    public decimal LatitudCentro { get; init; }
    public decimal LongitudCentro { get; init; }
}

internal sealed class CreateDistrictCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateDistrictCommand, DistrictDto>
{
    public async Task<DistrictDto> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.Distrito
        {
            NombreDistrito = request.NombreDistrito,
            Ubigeo = request.Ubigeo,
            LatitudCentro = request.LatitudCentro,
            LongitudCentro = request.LongitudCentro
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.Distrito>().AddAsync(entity, cancellationToken);
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

