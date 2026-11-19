using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Reviews.Commands;

public sealed record CreateReviewCommand : IRequest<ReviewDetailDto>
{
    public int IdInstitucion { get; init; }
    public int IdUsuario { get; init; }
    public short Puntuacion { get; init; }
    public string Comentario { get; init; } = string.Empty;
}

internal sealed class CreateReviewCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateReviewCommand, ReviewDetailDto>
{
    public async Task<ReviewDetailDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.Resena
        {
            IdInstitucion = request.IdInstitucion,
            IdUsuario = request.IdUsuario,
            Puntuacion = request.Puntuacion,
            Comentario = request.Comentario,
            Visible = true,
            FechaPublicacion = DateTime.UtcNow
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReviewDetailDto
        {
            IdResena = entity.IdResena,
            IdInstitucion = entity.IdInstitucion,
            IdUsuario = entity.IdUsuario,
            Puntuacion = entity.Puntuacion,
            Comentario = entity.Comentario,
            Visible = entity.Visible,
            FechaPublicacion = entity.FechaPublicacion,
            FechaActualizacion = entity.FechaActualizacion
        };
    }
}
