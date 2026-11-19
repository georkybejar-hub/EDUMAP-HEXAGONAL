using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

public sealed record UpdateInstitutionMediaCommand : IRequest<InstitutionMediaDto?>
{
    public int IdMultimedia { get; init; }
    public int IdInstitucion { get; init; }
    public string UrlArchivo { get; init; } = string.Empty;
    public string TipoMedio { get; init; } = string.Empty;
    public string TituloImagen { get; init; } = string.Empty;
    public short? OrdenVisual { get; init; }
}

internal sealed class UpdateInstitutionMediaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateInstitutionMediaCommand, InstitutionMediaDto?>
{
    public async Task<InstitutionMediaDto?> Handle(
        UpdateInstitutionMediaCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.MultimediaInstitucion>();

        var entity = await repository.GetByIdAsync(request.IdMultimedia, cancellationToken);
        if (entity is null) return null;

        entity.IdInstitucion = request.IdInstitucion;
        entity.UrlArchivo = request.UrlArchivo;
        entity.TipoMedio = request.TipoMedio;
        entity.TituloImagen = request.TituloImagen;
        entity.OrdenVisual = request.OrdenVisual;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new InstitutionMediaDto
        {
            IdMultimedia = entity.IdMultimedia,
            IdInstitucion = entity.IdInstitucion,
            UrlArchivo = entity.UrlArchivo,
            TipoMedio = entity.TipoMedio,
            TituloImagen = entity.TituloImagen,
            OrdenVisual = entity.OrdenVisual
        };
    }
}
