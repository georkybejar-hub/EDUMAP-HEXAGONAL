using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;

public sealed record UpdateEducationalOfferCommand : IRequest<EducationalOfferDto?>
{
    public int IdOferta { get; init; }
    public int IdSede { get; init; }
    public int IdPrograma { get; init; }
    public short IdModalidad { get; init; }
    public short IdTurno { get; init; }
    public decimal CostoInscripcion { get; init; }
    public decimal CostoMatricula { get; init; }
    public decimal PensionMensual { get; init; }
    public short CuotasAnuales { get; init; }
    public short AnioAcademico { get; init; }
}

internal sealed class UpdateEducationalOfferCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEducationalOfferCommand, EducationalOfferDto?>
{
    public async Task<EducationalOfferDto?> Handle(UpdateEducationalOfferCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>();

        var entity = await repository.GetByIdAsync(request.IdOferta, cancellationToken);
        if (entity is null) return null;

        entity.IdSede = request.IdSede;
        entity.IdPrograma = request.IdPrograma;
        entity.IdModalidad = request.IdModalidad;
        entity.IdTurno = request.IdTurno;
        entity.CostoInscripcion = request.CostoInscripcion;
        entity.CostoMatricula = request.CostoMatricula;
        entity.PensionMensual = request.PensionMensual;
        entity.CuotasAnuales = request.CuotasAnuales;
        entity.AnioAcademico = request.AnioAcademico;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

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
