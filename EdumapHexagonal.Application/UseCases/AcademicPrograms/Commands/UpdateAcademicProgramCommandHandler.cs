using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

internal sealed class UpdateAcademicProgramCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateAcademicProgramCommand, AcademicProgramDto?>
{
    public async Task<AcademicProgramDto?> Handle(UpdateAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>().GetByIdAsync(request.IdPrograma);
        if (entity == null) return null;

        entity.IdCategoria = request.IdCategoria;
        entity.NombrePrograma = request.NombrePrograma;
        entity.AreaAcademica = request.AreaAcademica;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new AcademicProgramDto
        {
            IdPrograma = e.IdPrograma,
            IdCategoria = e.IdCategoria,
            NombrePrograma = e.NombrePrograma,
            AreaAcademica = e.AreaAcademica
        };
    }
}
