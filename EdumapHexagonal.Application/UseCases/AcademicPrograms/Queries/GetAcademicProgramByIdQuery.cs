using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Queries;

public sealed record GetAcademicProgramByIdQuery(int Id) : IRequest<AcademicProgramDto?>;

internal sealed class GetAcademicProgramByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAcademicProgramByIdQuery, AcademicProgramDto?>
{
    public async Task<AcademicProgramDto?> Handle(GetAcademicProgramByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (e is null) return null;

        return new AcademicProgramDto
        {
            IdPrograma = e.IdPrograma,
            IdCategoria = e.IdCategoria,
            NombrePrograma = e.NombrePrograma,
            AreaAcademica = e.AreaAcademica
        };
    }
}
