using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

public sealed record CreateAcademicProgramCommand : IRequest<AcademicProgramDto>
{
    public short IdCategoria { get; init; }
    public string NombrePrograma { get; init; } = string.Empty;
    public string AreaAcademica { get; init; } = string.Empty;
}
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
    
    