using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;

public sealed record CreateEducationalOfferCommand : IRequest<EducationalOfferDto>
{
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

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.OfertasEducativa>().AddAsync(entity, cancellationToken);
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
