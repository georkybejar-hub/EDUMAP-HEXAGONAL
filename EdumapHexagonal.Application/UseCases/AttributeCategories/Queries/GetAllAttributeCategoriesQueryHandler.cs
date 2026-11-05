using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Queries;

internal sealed class GetAllAttributeCategoriesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllAttributeCategoriesQuery, IEnumerable<AttributeCategoryDto>>
{
    public async Task<IEnumerable<AttributeCategoryDto>> Handle(GetAllAttributeCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().GetAllAsync();
        return entities.Select(e => new AttributeCategoryDto
        {
            IdCatAtributo = e.IdCatAtributo,
            NombreCatAtributo = e.NombreCatAtributo,
            IconoUi = e.IconoUi
        });
    }
}
