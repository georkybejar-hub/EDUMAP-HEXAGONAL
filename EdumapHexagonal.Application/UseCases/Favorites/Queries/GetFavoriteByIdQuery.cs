using MediatR;
using EdumapHexagonal.Application.DTOs.Favorites;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Favorites.Queries;

public sealed record GetFavoriteByIdQuery : IRequest<FavoriteDto?>
{
    public int IdUsuario { get; init; }
    public int IdInstitucion { get; init; }
}

internal sealed class GetFavoriteByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetFavoriteByIdQuery, FavoriteDto?>
{
    public async Task<FavoriteDto?> Handle(GetFavoriteByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.FavoritosComparador>()
            .FirstOrDefaultAsync(
                f => f.IdUsuario == request.IdUsuario && f.IdInstitucion == request.IdInstitucion,
                cancellationToken);

        if (entity is null) return null;

        return new FavoriteDto
        {
            IdUsuario = entity.IdUsuario,
            IdInstitucion = entity.IdInstitucion,
            FechaAgregado = entity.FechaAgregado
        };
    }
}
