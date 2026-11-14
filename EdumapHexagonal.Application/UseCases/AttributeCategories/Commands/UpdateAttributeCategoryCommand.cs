using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

public sealed record UpdateAttributeCategoryCommand : IRequest<AttributeCategoryDto?>
{
    public short IdCatAtributo { get; init; }
    public string NombreCatAtributo { get; init; } = string.Empty;
    public string? IconoUi { get; init; }
}

internal sealed class UpdateAttributeCategoryCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAttributeCategoryCommand, AttributeCategoryDto?>
{
    public async Task<AttributeCategoryDto?> Handle(UpdateAttributeCategoryCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>();

        var entity = await repository.GetByIdAsync(request.IdCatAtributo, cancellationToken);
        if (entity is null) return null;

        entity.NombreCatAtributo = request.NombreCatAtributo;
        entity.IconoUi = request.IconoUi;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AttributeCategoryDto
        {
            IdCatAtributo = entity.IdCatAtributo,
            NombreCatAtributo = entity.NombreCatAtributo,
            IconoUi = entity.IconoUi
        };
    }
}
