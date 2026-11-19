using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Queries;

public sealed record GetInstitutionAttributesByInstitutionQuery : IRequest<IReadOnlyList<InstitutionAttributeDto>>
{
    public int IdInstitucion { get; init; }
}

internal sealed class GetInstitutionAttributesByInstitutionQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInstitutionAttributesByInstitutionQuery, IReadOnlyList<InstitutionAttributeDto>>
{
    public async Task<IReadOnlyList<InstitutionAttributeDto>> Handle(GetInstitutionAttributesByInstitutionQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.InstitucionAtributo>()
            .GetAllAsync(cancellationToken);

        return entities
            .Where(x => x.IdInstitucion == request.IdInstitucion)
            .Select(e => new InstitutionAttributeDto
            {
                IdInstitucion = e.IdInstitucion,
                IdAtributo = e.IdAtributo,
                DetalleValor = e.DetalleValor
            })
            .ToList();
    }
}

