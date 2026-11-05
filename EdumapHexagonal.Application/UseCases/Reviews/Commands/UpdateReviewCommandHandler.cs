using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Reviews.Commands;

internal sealed class UpdateReviewCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateReviewCommand, ReviewDto?>
{
    public async Task<ReviewDto?> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>().GetByIdAsync(request.IdResena);
        if (entity == null) return null;

        entity.IdInstitucion = request.IdInstitucion;
        entity.IdUsuario = request.IdUsuario;
        entity.Puntuacion = request.Puntuacion;
        entity.Comentario = request.Comentario;
        entity.Visible = request.Visible;
        entity.FechaPublicacion = request.FechaPublicacion;
        entity.FechaActualizacion = request.FechaActualizacion;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Resena>().Update(entity);
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
