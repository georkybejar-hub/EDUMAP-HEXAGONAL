using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Queries;

internal sealed class GetInstitutionMediaByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetInstitutionMediaByIdQuery, InstitutionMediaDto?>
{
    public async Task<InstitutionMediaDto?> Handle(GetInstitutionMediaByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>().GetByIdAsync(request.Id);
        if (e == null) return null;

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
