using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Reviews.Commands;

public sealed record UpdateReviewCommand : IRequest<ReviewDetailDto?>
{
    public int IdResena { get; init; }
    public short Puntuacion { get; init; }
    public string Comentario { get; init; } = string.Empty;
    public bool? Visible { get; init; }
}

internal sealed class UpdateReviewCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateReviewCommand, ReviewDetailDto?>
{
    public async Task<ReviewDetailDto?> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>();

        var entity = await repository.GetByIdAsync(request.IdResena, cancellationToken);
        if (entity is null) return null;

        entity.Puntuacion = request.Puntuacion;
        entity.Comentario = request.Comentario;
        entity.Visible = request.Visible ?? entity.Visible;
        entity.FechaActualizacion = DateTime.UtcNow;

        repository.Update(entity);
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
