using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Queries;

public sealed record GetInstitutionCategoryByIdQuery : IRequest<InstitutionCategoryDto?>
{
    public short IdCategoria { get; init; }
}

internal sealed class GetInstitutionCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInstitutionCategoryByIdQuery, InstitutionCategoryDto?>
{
    public async Task<InstitutionCategoryDto?> Handle(
        GetInstitutionCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.CategoriasInstitucion>()
            .GetByIdAsync(request.IdCategoria, cancellationToken);

        if (entity is null) return null;

        return new InstitutionCategoryDto
        {
            IdCategoria = entity.IdCategoria,
            NombreCategoria = entity.NombreCategoria,
            NivelEducativo = entity.NivelEducativo
        };
    }
}
