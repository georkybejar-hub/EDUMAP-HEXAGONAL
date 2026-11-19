using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Queries;

public sealed record GetAllEducationalOffersQuery : IRequest<IReadOnlyList<EducationalOfferDto>>;

internal sealed class GetAllEducationalOffersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllEducationalOffersQuery, IReadOnlyList<EducationalOfferDto>>
{
    public async Task<IReadOnlyList<EducationalOfferDto>> Handle(GetAllEducationalOffersQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.OfertasEducativa>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new EducationalOfferDto
            {
                IdOferta = e.IdOferta,
                IdSede = e.IdSede,
                IdPrograma = e.IdPrograma,
                IdModalidad = e.IdModalidad,
                IdTurno = e.IdTurno,
                CostoInscripcion = e.CostoInscripcion,
                CostoMatricula = e.CostoMatricula,
                PensionMensual = e.PensionMensual,
                CuotasAnuales = e.CuotasAnuales,
                AnioAcademico = e.AnioAcademico
            })
            .ToList();
    }
}

