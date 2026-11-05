using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

internal sealed class CreateInstitutionMediaCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateInstitutionMediaCommand, InstitutionMediaDto>
{
    public async Task<InstitutionMediaDto> Handle(CreateInstitutionMediaCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.MultimediaInstitucion
        {
            IdInstitucion = request.IdInstitucion,
            UrlArchivo = request.UrlArchivo,
            TipoMedio = request.TipoMedio,
            TituloImagen = request.TituloImagen,
            OrdenVisual = request.OrdenVisual
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>().AddAsync(entity);
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
