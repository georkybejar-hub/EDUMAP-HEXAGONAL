using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Commands;

public sealed record DeleteInstitutionAttributeCommand : IRequest<bool>
{
    public int IdInstitucion { get; init; }
    public int IdAtributo { get; init; }
}

internal sealed class DeleteInstitutionAttributeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteInstitutionAttributeCommand, bool>
{
    public async Task<bool> Handle(DeleteInstitutionAttributeCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.InstitucionAtributo>();

        var entity = await repository.FirstOrDefaultAsync(
            x => x.IdInstitucion == request.IdInstitucion && x.IdAtributo == request.IdAtributo,
            cancellationToken);

        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

