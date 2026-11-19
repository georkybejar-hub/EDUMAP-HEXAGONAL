using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Commands;

public sealed record CreateInstitutionAttributeCommand : IRequest<InstitutionAttributeDto>
{
    public int IdInstitucion { get; init; }
    public int IdAtributo { get; init; }
    public string? DetalleValor { get; init; }
}

internal sealed class CreateInstitutionAttributeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInstitutionAttributeCommand, InstitutionAttributeDto>
{
    public async Task<InstitutionAttributeDto> Handle(CreateInstitutionAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.InstitucionAtributo
        {
            IdInstitucion = request.IdInstitucion,
            IdAtributo = request.IdAtributo,
            DetalleValor = request.DetalleValor
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.InstitucionAtributo>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new InstitutionAttributeDto
        {
            IdInstitucion = entity.IdInstitucion,
            IdAtributo = entity.IdAtributo,
            DetalleValor = entity.DetalleValor
        };
    }
}

