using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

public sealed record CreateInstitutionMediaCommand : IRequest<InstitutionMediaDto>
{
    public int IdInstitucion { get; init; }
    public string UrlArchivo { get; init; } = string.Empty;
    public string TipoMedio { get; init; } = string.Empty;
    public string TituloImagen { get; init; } = string.Empty;
    public short? OrdenVisual { get; init; }
}

internal sealed class CreateInstitutionMediaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInstitutionMediaCommand, InstitutionMediaDto>
{
    public async Task<InstitutionMediaDto> Handle(
        CreateInstitutionMediaCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.MultimediaInstitucion
        {
            IdInstitucion = request.IdInstitucion,
            UrlArchivo = request.UrlArchivo,
            TipoMedio = request.TipoMedio,
            TituloImagen = request.TituloImagen,
            OrdenVisual = request.OrdenVisual
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.MultimediaInstitucion>()
            .AddAsync(entity, cancellationToken);

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