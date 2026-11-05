using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

internal sealed class CreateInstitutionCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateInstitutionCategoryCommand, InstitutionCategoryDto>
{
    public async Task<InstitutionCategoryDto> Handle(CreateInstitutionCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.CategoriasInstitucion
        {
            NombreCategoria = request.NombreCategoria,
            NivelEducativo = request.NivelEducativo
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new InstitutionCategoryDto
        {
            IdCategoria = e.IdCategoria,
            NombreCategoria = e.NombreCategoria,
            NivelEducativo = e.NivelEducativo
        };
    }
}
