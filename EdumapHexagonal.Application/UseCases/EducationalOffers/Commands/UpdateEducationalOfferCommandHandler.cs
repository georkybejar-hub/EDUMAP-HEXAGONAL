using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;

internal sealed class UpdateEducationalOfferCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateEducationalOfferCommand, EducationalOfferDto?>
{
    public async Task<EducationalOfferDto?> Handle(UpdateEducationalOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().GetByIdAsync(request.IdOferta);
        if (entity == null) return null;

        entity.IdSede = request.IdSede;
        entity.IdPrograma = request.IdPrograma;
        entity.IdModalidad = request.IdModalidad;
        entity.IdTurno = request.IdTurno;
        entity.CostoInscripcion = request.CostoInscripcion;
        entity.CostoMatricula = request.CostoMatricula;
        entity.PensionMensual = request.PensionMensual;
        entity.CuotasAnuales = request.CuotasAnuales;
        entity.AnioAcademico = request.AnioAcademico;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().Update(entity);
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
