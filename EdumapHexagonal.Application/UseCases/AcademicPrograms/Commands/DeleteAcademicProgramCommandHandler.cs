using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

internal sealed class DeleteAcademicProgramCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAcademicProgramCommand, bool>
{
    public async Task<bool> Handle(DeleteAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
