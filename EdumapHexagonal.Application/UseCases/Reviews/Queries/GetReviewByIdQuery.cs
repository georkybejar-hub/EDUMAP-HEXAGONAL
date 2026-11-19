using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Reviews.Queries;

public sealed record GetReviewByIdQuery(int Id) : IRequest<ReviewDetailDto?>;

internal sealed class GetReviewByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetReviewByIdQuery, ReviewDetailDto?>
{
    public async Task<ReviewDetailDto?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Resena>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (e is null) return null;

        return new ReviewDetailDto
        {
            IdResena = e.IdResena,
            IdInstitucion = e.IdInstitucion,
            IdUsuario = e.IdUsuario,
            Puntuacion = e.Puntuacion,
            Comentario = e.Comentario,
            Visible = e.Visible,
            FechaPublicacion = e.FechaPublicacion,
            FechaActualizacion = e.FechaActualizacion
        };
    }
}

