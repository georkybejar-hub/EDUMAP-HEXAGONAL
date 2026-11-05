using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

internal sealed class CreateAcademicProgramCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateAcademicProgramCommand, AcademicProgramDto>
{
    public async Task<AcademicProgramDto> Handle(CreateAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.ProgramasAcademico
        {
            IdCategoria = request.IdCategoria,
            NombrePrograma = request.NombrePrograma,
            AreaAcademica = request.AreaAcademica
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>().AddAsync(entity);
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
