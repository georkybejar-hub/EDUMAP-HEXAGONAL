using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

internal sealed class UpdateAttributeCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateAttributeCategoryCommand, AttributeCategoryDto?>
{
    public async Task<AttributeCategoryDto?> Handle(UpdateAttributeCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().GetByIdAsync(request.IdCatAtributo);
        if (entity == null) return null;

        entity.NombreCatAtributo = request.NombreCatAtributo;
        entity.IconoUi = request.IconoUi;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new AttributeCategoryDto
        {
            IdCatAtributo = e.IdCatAtributo,
            NombreCatAtributo = e.NombreCatAtributo,
            IconoUi = e.IconoUi
        };
    }
}
