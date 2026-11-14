using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Queries;

public sealed record GetAttributeCategoryByIdQuery : IRequest<AttributeCategoryDto?>
{
    public short IdCatAtributo { get; init; }
}

internal sealed class GetAttributeCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAttributeCategoryByIdQuery, AttributeCategoryDto?>
{
    public async Task<AttributeCategoryDto?> Handle(GetAttributeCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>()
            .GetByIdAsync(request.IdCatAtributo, cancellationToken);

        if (entity is null) return null;

        return new AttributeCategoryDto
        {
            IdCatAtributo = entity.IdCatAtributo,
            NombreCatAtributo = entity.NombreCatAtributo,
            IconoUi = entity.IconoUi
        };
    }
}
