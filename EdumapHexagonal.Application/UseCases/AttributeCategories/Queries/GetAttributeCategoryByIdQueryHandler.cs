using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Queries;

internal sealed class GetAttributeCategoryByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAttributeCategoryByIdQuery, AttributeCategoryDto?>
{
    public async Task<AttributeCategoryDto?> Handle(GetAttributeCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new AttributeCategoryDto
        {
            IdCatAtributo = e.IdCatAtributo,
            NombreCatAtributo = e.NombreCatAtributo,
            IconoUi = e.IconoUi
        };
    }
}
