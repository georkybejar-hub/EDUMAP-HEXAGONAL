using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

public sealed record UpdateAcademicProgramCommand : IRequest<AcademicProgramDto?>
{
    public int IdPrograma { get; init; }
    public short IdCategoria { get; init; }
    public string NombrePrograma { get; init; } = string.Empty;
    public string AreaAcademica { get; init; } = string.Empty;
}

internal sealed class UpdateAcademicProgramCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAcademicProgramCommand, AcademicProgramDto?>
{
    public async Task<AcademicProgramDto?> Handle(UpdateAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.ProgramasAcademico>();

        var entity = await repository.GetByIdAsync(request.IdPrograma, cancellationToken);
        if (entity is null) return null;

        entity.IdCategoria = request.IdCategoria;
        entity.NombrePrograma = request.NombrePrograma;
        entity.AreaAcademica = request.AreaAcademica;

        repository.Update(entity);
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
