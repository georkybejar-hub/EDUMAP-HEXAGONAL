using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Queries;

public sealed record GetAllInstitutionCategoriesQuery : IRequest<IReadOnlyList<InstitutionCategoryDto>>;

internal sealed class GetAllInstitutionCategoriesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllInstitutionCategoriesQuery, IReadOnlyList<InstitutionCategoryDto>>
{
    public async Task<IReadOnlyList<InstitutionCategoryDto>> Handle(
        GetAllInstitutionCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new InstitutionCategoryDto
            {
                IdCategoria = e.IdCategoria,
                NombreCategoria = e.NombreCategoria,
                NivelEducativo = e.NivelEducativo
            })
            .ToList();
    }
}