using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Queries;

public sealed record GetAllAcademicProgramsQuery : IRequest<IReadOnlyList<AcademicProgramDto>>;

internal sealed class GetAllAcademicProgramsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllAcademicProgramsQuery, IReadOnlyList<AcademicProgramDto>>
{
    public async Task<IReadOnlyList<AcademicProgramDto>> Handle(GetAllAcademicProgramsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new AcademicProgramDto
            {
                IdPrograma = e.IdPrograma,
                IdCategoria = e.IdCategoria,
                NombrePrograma = e.NombrePrograma,
                AreaAcademica = e.AreaAcademica
            })
            .ToList();
    }
}
