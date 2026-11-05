using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Queries;

internal sealed class GetAllInstitutionAttributesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllInstitutionAttributesQuery, IEnumerable<InstitutionAttributeDto>>
{
    public async Task<IEnumerable<InstitutionAttributeDto>> Handle(GetAllInstitutionAttributesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.InstitucionAtributo>().GetAllAsync();
        return entities.Select(e => new InstitutionAttributeDto
        {
            IdInstitucion = e.IdInstitucion,
            IdAtributo = e.IdAtributo,
            DetalleValor = e.DetalleValor
        });
    }
}
