using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

public sealed record DeleteInstitutionCommand : IRequest<bool>
{
    public int IdInstitucion { get; init; }
}

internal sealed class DeleteInstitutionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteInstitutionCommand, bool>
{
    public async Task<bool> Handle(
        DeleteInstitutionCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>();

        var entity = await repository.GetByIdAsync(request.IdInstitucion, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}