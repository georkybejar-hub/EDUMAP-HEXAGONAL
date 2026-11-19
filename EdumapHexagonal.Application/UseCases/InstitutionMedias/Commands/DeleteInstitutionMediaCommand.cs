using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

public sealed record DeleteInstitutionMediaCommand : IRequest<bool>
{
    public int IdMultimedia { get; init; }
}

internal sealed class DeleteInstitutionMediaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteInstitutionMediaCommand, bool>
{
    public async Task<bool> Handle(
        DeleteInstitutionMediaCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.MultimediaInstitucion>();

        var entity = await repository.GetByIdAsync(request.IdMultimedia, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
