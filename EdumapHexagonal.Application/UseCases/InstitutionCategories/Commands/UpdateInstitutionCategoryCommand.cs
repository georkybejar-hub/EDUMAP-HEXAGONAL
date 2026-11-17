using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

public sealed record UpdateInstitutionCategoryCommand : IRequest<InstitutionCategoryDto?>
{
    public short IdCategoria { get; init; }
    public string NombreCategoria { get; init; } = string.Empty;
    public string NivelEducativo { get; init; } = string.Empty;
}

internal sealed class UpdateInstitutionCategoryCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateInstitutionCategoryCommand, InstitutionCategoryDto?>
{
    public async Task<InstitutionCategoryDto?> Handle(
        UpdateInstitutionCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>();

        var entity = await repository.GetByIdAsync(request.IdCategoria, cancellationToken);
        if (entity is null) return null;

        entity.NombreCategoria = request.NombreCategoria;
        entity.NivelEducativo = request.NivelEducativo;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new InstitutionCategoryDto
        {
            IdCategoria = entity.IdCategoria,
            NombreCategoria = entity.NombreCategoria,
            NivelEducativo = entity.NivelEducativo
        };
    }
}