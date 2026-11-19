
using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

public sealed record CreateInstitutionCommand : IRequest<InstitutionDto>
{
    public short IdCategoria { get; init; }
    public short IdTipoGestion { get; init; }
    public short? IdReguladora { get; init; }
    public int? IdRepresentante { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string? CodigoModular { get; init; }
    public string? Ruc { get; init; }
    public string Descripcion { get; init; } = string.Empty;
    public string? SitioWeb { get; init; }
    public string? LogoUrl { get; init; }
    public bool? Verificada { get; init; }
}

internal sealed class CreateInstitutionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInstitutionCommand, InstitutionDto>
{
    public async Task<InstitutionDto> Handle(
        CreateInstitutionCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.Institucione
        {
            IdCategoria = request.IdCategoria,
            IdTipoGestion = request.IdTipoGestion,
            IdReguladora = request.IdReguladora,
            IdRepresentante = request.IdRepresentante,
            Nombre = request.Nombre,
            CodigoModular = request.CodigoModular,
            Ruc = request.Ruc,
            Descripcion = request.Descripcion,
            SitioWeb = request.SitioWeb,
            LogoUrl = request.LogoUrl,
            Verificada = request.Verificada,
            FechaCreacion = DateTime.UtcNow
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.Institucione>()
            .AddAsync(entity, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new InstitutionDto
        {
            IdInstitucion = entity.IdInstitucion,
            IdCategoria = entity.IdCategoria,
            IdTipoGestion = entity.IdTipoGestion,
            IdReguladora = entity.IdReguladora,
            IdRepresentante = entity.IdRepresentante,
            Nombre = entity.Nombre,
            CodigoModular = entity.CodigoModular,
            Ruc = entity.Ruc,
            Descripcion = entity.Descripcion,
            SitioWeb = entity.SitioWeb,
            LogoUrl = entity.LogoUrl,
            Verificada = entity.Verificada,
            FechaCreacion = entity.FechaCreacion
        };
    }
}

