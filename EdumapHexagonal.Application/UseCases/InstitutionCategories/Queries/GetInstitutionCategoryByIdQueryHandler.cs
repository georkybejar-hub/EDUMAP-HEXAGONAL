using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Queries;

internal sealed class GetInstitutionCategoryByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetInstitutionCategoryByIdQuery, InstitutionCategoryDto?>
{
    public async Task<InstitutionCategoryDto?> Handle(GetInstitutionCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new InstitutionCategoryDto
        {
            IdCategoria = e.IdCategoria,
            NombreCategoria = e.NombreCategoria,
            NivelEducativo = e.NivelEducativo
        };
    }
}
