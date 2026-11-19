using MediatR;
using EdumapHexagonal.Application.DTOs.Favorites;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Domain.Models;

namespace EdumapHexagonal.Application.UseCases.Favorites.Queries;

public sealed record GetFavoritesByUserQuery : IRequest<IReadOnlyList<FavoriteDto>>
{
    public int IdUsuario { get; init; }
}

internal sealed class GetFavoritesByUserQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetFavoritesByUserQuery, IReadOnlyList<FavoriteDto>>
{
    public async Task<IReadOnlyList<FavoriteDto>> Handle(GetFavoritesByUserQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<FavoritosComparador>()
            .GetAllAsync(f => f.IdUsuario == request.IdUsuario, cancellationToken);

        return entities
            .Select(e => new FavoriteDto
            {
                IdUsuario = e.IdUsuario,
                IdInstitucion = e.IdInstitucion,
                FechaAgregado = e.FechaAgregado
            })
            .ToList();
    }
}
