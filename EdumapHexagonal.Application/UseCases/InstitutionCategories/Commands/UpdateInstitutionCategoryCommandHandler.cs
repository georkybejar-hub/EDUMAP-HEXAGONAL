using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

internal sealed class UpdateInstitutionCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateInstitutionCategoryCommand, InstitutionCategoryDto?>
{
    public async Task<InstitutionCategoryDto?> Handle(UpdateInstitutionCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>().GetByIdAsync(request.IdCategoria);
        if (entity == null) return null;

        entity.NombreCategoria = request.NombreCategoria;
        entity.NivelEducativo = request.NivelEducativo;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>().Update(entity);
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
