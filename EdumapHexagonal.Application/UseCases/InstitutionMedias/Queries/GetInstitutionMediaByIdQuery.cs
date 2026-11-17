using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Queries;

public sealed record GetInstitutionMediaByIdQuery : IRequest<InstitutionMediaDto?>
{
    public int IdMultimedia { get; init; }
}

internal sealed class GetInstitutionMediaByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInstitutionMediaByIdQuery, InstitutionMediaDto?>
{
    public async Task<InstitutionMediaDto?> Handle(
        GetInstitutionMediaByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>()
            .GetByIdAsync(request.IdMultimedia, cancellationToken);

        if (entity is null) return null;

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