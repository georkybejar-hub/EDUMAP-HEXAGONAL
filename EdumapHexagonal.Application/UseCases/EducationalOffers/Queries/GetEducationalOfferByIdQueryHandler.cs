using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Queries;

internal sealed class GetEducationalOfferByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetEducationalOfferByIdQuery, EducationalOfferDto?>
{
    public async Task<EducationalOfferDto?> Handle(GetEducationalOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new EducationalOfferDto
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
        };
    }
}
