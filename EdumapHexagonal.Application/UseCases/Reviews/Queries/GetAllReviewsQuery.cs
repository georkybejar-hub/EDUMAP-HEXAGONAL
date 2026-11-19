using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Reviews.Queries;

public sealed record GetAllReviewsQuery : IRequest<IReadOnlyList<ReviewDetailDto>>;

internal sealed class GetAllReviewsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllReviewsQuery, IReadOnlyList<ReviewDetailDto>>
{
    public async Task<IReadOnlyList<ReviewDetailDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.Resena>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new ReviewDetailDto
            {
                IdResena = e.IdResena,
                IdInstitucion = e.IdInstitucion,
                IdUsuario = e.IdUsuario,
                Puntuacion = e.Puntuacion,
                Comentario = e.Comentario,
                Visible = e.Visible,
                FechaPublicacion = e.FechaPublicacion,
                FechaActualizacion = e.FechaActualizacion
            })
            .ToList();
    }
}
