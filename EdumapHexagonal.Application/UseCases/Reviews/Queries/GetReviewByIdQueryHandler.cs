using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Reviews.Queries;

internal sealed class GetReviewByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetReviewByIdQuery, ReviewDto?>
{
    public async Task<ReviewDto?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new ReviewDto
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
