using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;

namespace EdumapHexagonal.Application.UseCases.Reviews.Commands;

public sealed record CreateReviewCommand : IRequest<ReviewDto>
{
    public int IdInstitucion { get; init; }
    public int IdUsuario { get; init; }
    public short Puntuacion { get; init; }
    public string Comentario { get; init; } = string.Empty;
    public bool? Visible { get; init; }
    public DateTime? FechaPublicacion { get; init; }
    public DateTime? FechaActualizacion { get; init; }
}
