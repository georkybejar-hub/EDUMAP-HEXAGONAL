
using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

public sealed record UpdateInstitutionCommand : IRequest<InstitutionDto?>
{
    public int IdInstitucion { get; init; }
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

internal sealed class UpdateInstitutionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateInstitutionCommand, InstitutionDto?>
{
    public async Task<InstitutionDto?> Handle(
        UpdateInstitutionCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.Institucione>();

        var entity = await repository.GetByIdAsync(request.IdInstitucion, cancellationToken);
        if (entity is null) return null;

        entity.IdCategoria = request.IdCategoria;
        entity.IdTipoGestion = request.IdTipoGestion;
        entity.IdReguladora = request.IdReguladora;
        entity.IdRepresentante = request.IdRepresentante;
        entity.Nombre = request.Nombre;
        entity.CodigoModular = request.CodigoModular;
        entity.Ruc = request.Ruc;
        entity.Descripcion = request.Descripcion;
        entity.SitioWeb = request.SitioWeb;
        entity.LogoUrl = request.LogoUrl;
        entity.Verificada = request.Verificada;

        repository.Update(entity);
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

