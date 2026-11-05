using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Queries;

internal sealed class GetAllAcademicProgramsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllAcademicProgramsQuery, IEnumerable<AcademicProgramDto>>
{
    public async Task<IEnumerable<AcademicProgramDto>> Handle(GetAllAcademicProgramsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>().GetAllAsync();
        return entities.Select(e => new AcademicProgramDto
        {
            IdPrograma = e.IdPrograma,
            IdCategoria = e.IdCategoria,
            NombrePrograma = e.NombrePrograma,
            AreaAcademica = e.AreaAcademica
        });
    }
}
