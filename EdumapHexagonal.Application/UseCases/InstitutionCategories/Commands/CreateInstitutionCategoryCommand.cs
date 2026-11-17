
using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

public sealed record CreateInstitutionCategoryCommand : IRequest<InstitutionCategoryDto>
{
    public string NombreCategoria { get; init; } = string.Empty;
    public string NivelEducativo { get; init; } = string.Empty;
}

internal sealed class CreateInstitutionCategoryCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInstitutionCategoryCommand, InstitutionCategoryDto>
{
    public async Task<InstitutionCategoryDto> Handle(
        CreateInstitutionCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.CategoriasInstitucion
        {
            NombreCategoria = request.NombreCategoria,
            NivelEducativo = request.NivelEducativo
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>()
            .AddAsync(entity, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new InstitutionCategoryDto
        {
            IdCategoria = entity.IdCategoria,
            NombreCategoria = entity.NombreCategoria,
            NivelEducativo = entity.NivelEducativo
        };
    }
}
