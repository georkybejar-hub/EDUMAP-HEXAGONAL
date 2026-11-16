using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Queries;

public sealed record GetInstitutionAttributeByIdQuery : IRequest<InstitutionAttributeDto?>
{
    public int IdInstitucion { get; init; }
    public int IdAtributo { get; init; }
}

internal sealed class GetInstitutionAttributeByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInstitutionAttributeByIdQuery, InstitutionAttributeDto?>
{
    public async Task<InstitutionAttributeDto?> Handle(GetInstitutionAttributeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.InstitucionAtributo>()
            .FirstOrDefaultAsync(
                x => x.IdInstitucion == request.IdInstitucion && x.IdAtributo == request.IdAtributo,
                cancellationToken);

        if (entity is null) return null;

        return new InstitutionAttributeDto
        {
            IdInstitucion = entity.IdInstitucion,
            IdAtributo = entity.IdAtributo,
            DetalleValor = entity.DetalleValor
        };
    }
}
