using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;

internal sealed class CreateEducationalOfferCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateEducationalOfferCommand, EducationalOfferDto>
{
    public async Task<EducationalOfferDto> Handle(CreateEducationalOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.OfertasEducativa
        {
            IdSede = request.IdSede,
            IdPrograma = request.IdPrograma,
            IdModalidad = request.IdModalidad,
            IdTurno = request.IdTurno,
            CostoInscripcion = request.CostoInscripcion,
            CostoMatricula = request.CostoMatricula,
            PensionMensual = request.PensionMensual,
            CuotasAnuales = request.CuotasAnuales,
            AnioAcademico = request.AnioAcademico
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
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
