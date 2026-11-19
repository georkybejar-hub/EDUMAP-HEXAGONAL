using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Domain.Models;

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
        if (request.Puntuacion < 1 || request.Puntuacion > 5)
        {
            throw new ArgumentException("La puntuación debe estar comprendida entre 1 y 5 estrellas.");
        }

        var repository = unitOfWork.Repository<Resena>();

        var yaExiste = await repository.AnyAsync(
            r => r.IdInstitucion == request.IdInstitucion && r.IdUsuario == request.IdUsuario,
            cancellationToken);

        if (yaExiste)
        {
            throw new InvalidOperationException("El usuario ya ha registrado una reseña para esta institución.");
        }

        var entity = new Resena
        {
            IdInstitucion = request.IdInstitucion,
            IdUsuario = request.IdUsuario,
            Puntuacion = request.Puntuacion,
            Comentario = request.Comentario,
            Visible = true,
            FechaPublicacion = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
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
