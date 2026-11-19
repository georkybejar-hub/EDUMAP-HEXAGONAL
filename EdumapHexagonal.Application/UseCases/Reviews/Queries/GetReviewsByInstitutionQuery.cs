using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Application.UseCases.Reviews.Queries;

public sealed record GetReviewsByInstitutionQuery : IRequest<IReadOnlyList<ReviewDto>?>
{
    public int IdInstitucion { get; init; }
}

internal sealed class GetReviewsByInstitutionQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetReviewsByInstitutionQuery, IReadOnlyList<ReviewDto>?>
{
    public async Task<IReadOnlyList<ReviewDto>?> Handle(
        GetReviewsByInstitutionQuery request,
        CancellationToken cancellationToken)
    {
        var exists = await unitOfWork.Repository<Institucione>()
            .AnyAsync(e => e.IdInstitucion == request.IdInstitucion, cancellationToken);
        if (!exists)
            return null;

        var reviews = await unitOfWork.Repository<Resena>().GetAllAsync(
            e => e.IdInstitucion == request.IdInstitucion && e.Visible != false,
            e => new
            {
                e.IdResena,
                e.IdUsuarioNavigation.Nombres,
                e.IdUsuarioNavigation.Apellidos,
                e.Puntuacion,
                e.Comentario,
                e.FechaPublicacion,
                Editada = e.FechaActualizacion != null
            },
            cancellationToken);

        return reviews
            .OrderByDescending(e => e.FechaPublicacion)
            .ThenByDescending(e => e.IdResena)
            .Select(e => new ReviewDto
            {
                IdResena = e.IdResena,
                Autor = GetPublicAuthorName(e.Nombres, e.Apellidos),
                Puntuacion = e.Puntuacion,
                Comentario = e.Comentario,
                FechaPublicacion = e.FechaPublicacion,
                Editada = e.Editada
            })
            .ToList();
    }

    private static string GetPublicAuthorName(string nombres, string apellidos)
    {
        var nombre = nombres.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "Usuario";
        var inicial = apellidos.Trim().FirstOrDefault();
        return inicial == default ? nombre : $"{nombre} {char.ToUpperInvariant(inicial)}.";
    }
}
