using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Queries;

public sealed record GetAllInstitutionAttributesQuery : IRequest<IReadOnlyList<InstitutionAttributeDto>>;

internal sealed class GetAllInstitutionAttributesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllInstitutionAttributesQuery, IReadOnlyList<InstitutionAttributeDto>>
{
    public async Task<IReadOnlyList<InstitutionAttributeDto>> Handle(GetAllInstitutionAttributesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.InstitucionAtributo>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new InstitutionAttributeDto
            {
                IdInstitucion = e.IdInstitucion,
                IdAtributo = e.IdAtributo,
                DetalleValor = e.DetalleValor
            })
            .ToList();
    }
}

