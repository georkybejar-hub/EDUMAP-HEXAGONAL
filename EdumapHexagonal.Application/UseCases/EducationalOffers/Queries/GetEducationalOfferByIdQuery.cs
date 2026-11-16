using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Queries;

public sealed record GetEducationalOfferByIdQuery : IRequest<EducationalOfferDto?>
{
    public int IdOferta { get; init; }
}

internal sealed class GetEducationalOfferByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetEducationalOfferByIdQuery, EducationalOfferDto?>
{
    public async Task<EducationalOfferDto?> Handle(GetEducationalOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>()
            .GetByIdAsync(request.IdOferta, cancellationToken);

        if (entity is null) return null;

        return new EducationalOfferDto
        {
            IdOferta = entity.IdOferta,
            IdSede = entity.IdSede,
            IdPrograma = entity.IdPrograma,
            IdModalidad = entity.IdModalidad,
            IdTurno = entity.IdTurno,
            CostoInscripcion = entity.CostoInscripcion,
            CostoMatricula = entity.CostoMatricula,
            PensionMensual = entity.PensionMensual,
            CuotasAnuales = entity.CuotasAnuales,
            AnioAcademico = entity.AnioAcademico
        };
    }
}
