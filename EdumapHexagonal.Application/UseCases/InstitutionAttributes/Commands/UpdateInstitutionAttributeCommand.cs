using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Commands;

public sealed record UpdateInstitutionAttributeCommand : IRequest<InstitutionAttributeDto?>
{
    public int IdInstitucion { get; init; }
    public int IdAtributo { get; init; }
    public string? DetalleValor { get; init; }
}

internal sealed class UpdateInstitutionAttributeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateInstitutionAttributeCommand, InstitutionAttributeDto?>
{
    public async Task<InstitutionAttributeDto?> Handle(UpdateInstitutionAttributeCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.InstitucionAtributo>();

        var entity = await repository.FirstOrDefaultAsync(
            x => x.IdInstitucion == request.IdInstitucion && x.IdAtributo == request.IdAtributo,
            cancellationToken);

        if (entity is null) return null;

        entity.DetalleValor = request.DetalleValor;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new InstitutionAttributeDto
        {
            IdInstitucion = entity.IdInstitucion,
            IdAtributo = entity.IdAtributo,
            DetalleValor = entity.DetalleValor
        };
    }
}

