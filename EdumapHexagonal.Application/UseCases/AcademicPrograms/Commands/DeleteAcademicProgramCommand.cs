using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

public sealed record DeleteAcademicProgramCommand : IRequest<bool>
{
    public int IdPrograma { get; init; }
}

internal sealed class DeleteAcademicProgramCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAcademicProgramCommand, bool>
{
    public async Task<bool> Handle(DeleteAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>();

        var entity = await repository.GetByIdAsync(request.IdPrograma, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
