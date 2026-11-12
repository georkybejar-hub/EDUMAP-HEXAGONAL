using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

public sealed record CreateAcademicProgramCommand : IRequest<AcademicProgramDto>
{
    public short IdCategoria { get; init; }
    public string NombrePrograma { get; init; } = string.Empty;
    public string AreaAcademica { get; init; } = string.Empty;
}

internal sealed class CreateAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateAcademicProgramCommand, AcademicProgramDto>
{
    public async Task<AcademicProgramDto> Handle(
        CreateAcademicProgramCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.ProgramasAcademico
        {
            IdCategoria = request.IdCategoria,
            NombrePrograma = request.NombrePrograma,
            AreaAcademica = request.AreaAcademica
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>()
            .AddAsync(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AcademicProgramDto
        {
            IdPrograma = entity.IdPrograma,
            IdCategoria = entity.IdCategoria,
            NombrePrograma = entity.NombrePrograma,
            AreaAcademica = entity.AreaAcademica
        };
    }
}