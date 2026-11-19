using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Institutions.Queries;

public sealed record GetInstitutionByIdQuery : IRequest<InstitutionDto?>
{
    public int IdInstitucion { get; init; }
}

internal sealed class GetInstitutionByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInstitutionByIdQuery, InstitutionDto?>
{
    public async Task<InstitutionDto?> Handle(
        GetInstitutionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Institucione>()
            .GetByIdAsync(request.IdInstitucion, cancellationToken);

        if (entity is null) return null;

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
