using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

internal sealed class CreateAttributeCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateAttributeCategoryCommand, AttributeCategoryDto>
{
    public async Task<AttributeCategoryDto> Handle(CreateAttributeCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.CategoriasAtributo
        {
            NombreCatAtributo = request.NombreCatAtributo,
            IconoUi = request.IconoUi
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().AddAsync(entity);
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
