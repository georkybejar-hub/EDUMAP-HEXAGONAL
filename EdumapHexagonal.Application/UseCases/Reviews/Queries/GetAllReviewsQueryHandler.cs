using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Reviews.Queries;

internal sealed class GetAllReviewsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>().GetAllAsync();
        return entities.Select(e => new ReviewDto
        {
            IdResena = e.IdResena,
            IdInstitucion = e.IdInstitucion,
            IdUsuario = e.IdUsuario,
            Puntuacion = e.Puntuacion,
            Comentario = e.Comentario,
            Visible = e.Visible,
            FechaPublicacion = e.FechaPublicacion,
            FechaActualizacion = e.FechaActualizacion
        });
    }
}
