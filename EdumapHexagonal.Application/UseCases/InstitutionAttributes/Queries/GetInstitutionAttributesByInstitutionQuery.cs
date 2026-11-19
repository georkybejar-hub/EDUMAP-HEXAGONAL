using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Domain.Models;

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
            .Repository<InstitucionAtributo>()
            .GetAllAsync(x => x.IdInstitucion == request.IdInstitucion, cancellationToken);

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
