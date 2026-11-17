using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Queries;

public sealed record GetAllInstitutionMediasQuery : IRequest<IReadOnlyList<InstitutionMediaDto>>;

internal sealed class GetAllInstitutionMediasQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllInstitutionMediasQuery, IReadOnlyList<InstitutionMediaDto>>
{
    public async Task<IReadOnlyList<InstitutionMediaDto>> Handle(
        GetAllInstitutionMediasQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new InstitutionMediaDto
            {
                IdMultimedia = e.IdMultimedia,
                IdInstitucion = e.IdInstitucion,
                UrlArchivo = e.UrlArchivo,
                TipoMedio = e.TipoMedio,
                TituloImagen = e.TituloImagen,
                OrdenVisual = e.OrdenVisual
            })
            .ToList();
    }
}