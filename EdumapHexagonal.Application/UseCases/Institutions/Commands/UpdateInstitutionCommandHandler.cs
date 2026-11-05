using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

internal sealed class UpdateInstitutionCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateInstitutionCommand, InstitutionDto?>
{
    public async Task<InstitutionDto?> Handle(UpdateInstitutionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>().GetByIdAsync(request.IdInstitucion);
        if (entity == null) return null;

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
        entity.FechaCreacion = request.FechaCreacion;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new InstitutionDto
        {
            IdInstitucion = e.IdInstitucion,
            IdCategoria = e.IdCategoria,
            IdTipoGestion = e.IdTipoGestion,
            IdReguladora = e.IdReguladora,
            IdRepresentante = e.IdRepresentante,
            Nombre = e.Nombre,
            CodigoModular = e.CodigoModular,
            Ruc = e.Ruc,
            Descripcion = e.Descripcion,
            SitioWeb = e.SitioWeb,
            LogoUrl = e.LogoUrl,
            Verificada = e.Verificada,
            FechaCreacion = e.FechaCreacion
        };
    }
}
