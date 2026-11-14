using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Queries;

public sealed record GetAllAttributeCategoriesQuery : IRequest<IReadOnlyList<AttributeCategoryDto>>;

internal sealed class GetAllAttributeCategoriesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllAttributeCategoriesQuery, IReadOnlyList<AttributeCategoryDto>>
{
    public async Task<IReadOnlyList<AttributeCategoryDto>> Handle(GetAllAttributeCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new AttributeCategoryDto
            {
                IdCatAtributo = e.IdCatAtributo,
                NombreCatAtributo = e.NombreCatAtributo,
                IconoUi = e.IconoUi
            })
            .ToList();
    }
}
