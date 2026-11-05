using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

internal sealed class CreateInstitutionCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateInstitutionCommand, InstitutionDto>
{
    public async Task<InstitutionDto> Handle(CreateInstitutionCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.Institucione
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
            FechaCreacion = request.FechaCreacion
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Institucione>().AddAsync(entity);
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
