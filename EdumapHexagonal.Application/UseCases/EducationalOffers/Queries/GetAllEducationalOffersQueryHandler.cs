using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Queries;

internal sealed class GetAllEducationalOffersQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllEducationalOffersQuery, IEnumerable<EducationalOfferDto>>
{
    public async Task<IEnumerable<EducationalOfferDto>> Handle(GetAllEducationalOffersQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().GetAllAsync();
        return entities.Select(e => new EducationalOfferDto
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
        });
    }
}
