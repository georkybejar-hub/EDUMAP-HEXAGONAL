using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Queries;

internal sealed class GetAllInstitutionMediasQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllInstitutionMediasQuery, IEnumerable<InstitutionMediaDto>>
{
    public async Task<IEnumerable<InstitutionMediaDto>> Handle(GetAllInstitutionMediasQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>().GetAllAsync();
        return entities.Select(e => new InstitutionMediaDto
        {
            IdMultimedia = e.IdMultimedia,
            IdInstitucion = e.IdInstitucion,
            UrlArchivo = e.UrlArchivo,
            TipoMedio = e.TipoMedio,
            TituloImagen = e.TituloImagen,
            OrdenVisual = e.OrdenVisual
        });
    }
}
