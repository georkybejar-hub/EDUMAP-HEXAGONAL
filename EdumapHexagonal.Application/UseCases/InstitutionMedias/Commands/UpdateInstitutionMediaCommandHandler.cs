using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

internal sealed class UpdateInstitutionMediaCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateInstitutionMediaCommand, InstitutionMediaDto?>
{
    public async Task<InstitutionMediaDto?> Handle(UpdateInstitutionMediaCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>().GetByIdAsync(request.IdMultimedia);
        if (entity == null) return null;

        entity.IdInstitucion = request.IdInstitucion;
        entity.UrlArchivo = request.UrlArchivo;
        entity.TipoMedio = request.TipoMedio;
        entity.TituloImagen = request.TituloImagen;
        entity.OrdenVisual = request.OrdenVisual;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new InstitutionMediaDto
        {
            IdMultimedia = e.IdMultimedia,
            IdInstitucion = e.IdInstitucion,
            UrlArchivo = e.UrlArchivo,
            TipoMedio = e.TipoMedio,
            TituloImagen = e.TituloImagen,
            OrdenVisual = e.OrdenVisual
        };
    }
}
