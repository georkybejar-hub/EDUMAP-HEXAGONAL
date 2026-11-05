using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;

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
