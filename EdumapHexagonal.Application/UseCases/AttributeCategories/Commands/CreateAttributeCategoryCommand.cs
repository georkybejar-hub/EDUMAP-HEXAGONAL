using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

public sealed record CreateAttributeCategoryCommand : IRequest<AttributeCategoryDto>
{
    public string NombreCatAtributo { get; init; } = string.Empty;
    public string? IconoUi { get; init; }
}

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

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AttributeCategoryDto
        {
            IdCatAtributo = entity.IdCatAtributo,
            NombreCatAtributo = entity.NombreCatAtributo,
            IconoUi = entity.IconoUi
        };
    }
}
