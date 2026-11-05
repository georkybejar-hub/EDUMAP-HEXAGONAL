using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Queries;

internal sealed class GetAllInstitutionCategoriesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllInstitutionCategoriesQuery, IEnumerable<InstitutionCategoryDto>>
{
    public async Task<IEnumerable<InstitutionCategoryDto>> Handle(GetAllInstitutionCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>().GetAllAsync();
        return entities.Select(e => new InstitutionCategoryDto
        {
            IdCategoria = e.IdCategoria,
            NombreCategoria = e.NombreCategoria,
            NivelEducativo = e.NivelEducativo
        });
    }
}
