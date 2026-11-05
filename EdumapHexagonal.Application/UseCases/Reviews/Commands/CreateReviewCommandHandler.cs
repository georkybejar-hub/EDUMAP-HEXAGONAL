using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Reviews.Commands;

internal sealed class CreateReviewCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.Resena
        {
            IdInstitucion = request.IdInstitucion,
            IdUsuario = request.IdUsuario,
            Puntuacion = request.Puntuacion,
            Comentario = request.Comentario,
            Visible = request.Visible,
            FechaPublicacion = request.FechaPublicacion,
            FechaActualizacion = request.FechaActualizacion
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
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
