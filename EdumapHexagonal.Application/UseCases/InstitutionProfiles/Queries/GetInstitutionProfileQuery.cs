using EdumapHexagonal.Application.DTOs.InstitutionProfiles;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Domain.Models;
using MediatR;

namespace EdumapHexagonal.Application.UseCases.InstitutionProfiles.Queries;

public sealed record GetInstitutionProfileQuery(int IdInstitucion)
    : IRequest<InstitutionProfileDto?>;

internal sealed class GetInstitutionProfileQueryHandler(
    IUnitOfWork unitOfWork, IEstimatedOfferReader estimatedOfferReader)
    : IRequestHandler<GetInstitutionProfileQuery, InstitutionProfileDto?>
{
    public async Task<InstitutionProfileDto?> Handle(
        GetInstitutionProfileQuery request, CancellationToken cancellationToken)
    {
        var institution = await unitOfWork.Repository<Institucione>()
            .FirstOrDefaultAsync(x => x.IdInstitucion == request.IdInstitucion, cancellationToken);
        if (institution is null) return null;

        var campuses = await unitOfWork.Repository<SedesInstitucion>().GetAllAsync(
            x => x.IdInstitucion == request.IdInstitucion,
            x => new InstitutionCampusProfileDto
            {
                IdSede = x.IdSede,
                Nombre = x.NombreSede,
                Direccion = x.Direccion,
                Distrito = x.IdDistritoNavigation.NombreDistrito,
                EsPrincipal = x.EsPrincipal == true
            }, cancellationToken);

        var campusIds = campuses.Select(x => x.IdSede).ToArray();
        IReadOnlyList<InstitutionOfferProfileDto> offers = [];
        IReadOnlyList<InstitutionContactProfileDto> contacts = [];

        if (campusIds.Length > 0)
        {
            offers = await unitOfWork.Repository<OfertasEducativa>().GetAllAsync(
                x => campusIds.Contains(x.IdSede),
                x => new InstitutionOfferProfileDto
                {
                    IdOferta = x.IdOferta,
                    IdSede = x.IdSede,
                    NombreSede = x.IdSedeNavigation.NombreSede,
                    Programa = x.IdProgramaNavigation.NombrePrograma,
                    AreaAcademica = x.IdProgramaNavigation.AreaAcademica,
                    Modalidad = x.IdModalidadNavigation.NombreModalidad,
                    Turno = x.IdTurnoNavigation.NombreTurno,
                    CostoInscripcion = x.CostoInscripcion,
                    CostoMatricula = x.CostoMatricula,
                    PensionMensual = x.PensionMensual,
                    AnioAcademico = x.AnioAcademico
                }, cancellationToken);

            contacts = await unitOfWork.Repository<CanalesContactoSede>().GetAllAsync(
                x => campusIds.Contains(x.IdSede),
                x => new InstitutionContactProfileDto
                {
                    IdContacto = x.IdContacto,
                    IdSede = x.IdSede,
                    TipoCanal = x.TipoCanal,
                    ValorContacto = x.ValorContacto,
                    AreaAtencion = x.AreaAtencion,
                    HorarioAtencion = x.HorarioAtencion
                }, cancellationToken);
        }

        var estimatedIds = await estimatedOfferReader.GetEstimatedOfferIdsAsync(
            offers.Select(x => x.IdOferta).ToArray(), cancellationToken);
        offers = offers.Select(x => x with { EsEstimado = estimatedIds.Contains(x.IdOferta) })
            .OrderBy(x => x.Programa)
            .ThenByDescending(x => x.AnioAcademico)
            .ThenBy(x => x.NombreSede)
            .ToList();

        var attributes = await unitOfWork.Repository<InstitucionAtributo>().GetAllAsync(
            x => x.IdInstitucion == request.IdInstitucion,
            x => new InstitutionAttributeProfileDto
            {
                Nombre = x.IdAtributoNavigation.NombreAtributo,
                Categoria = x.IdAtributoNavigation.IdCatAtributoNavigation.NombreCatAtributo,
                Valor = x.DetalleValor
            }, cancellationToken);

        var media = await unitOfWork.Repository<MultimediaInstitucion>().GetAllAsync(
            x => x.IdInstitucion == request.IdInstitucion,
            x => new InstitutionMediaProfileDto
            {
                Url = x.UrlArchivo,
                Tipo = x.TipoMedio,
                Titulo = x.TituloImagen,
                Orden = x.OrdenVisual ?? 1
            }, cancellationToken);

        return new InstitutionProfileDto
        {
            IdInstitucion = institution.IdInstitucion,
            Nombre = institution.Nombre,
            Descripcion = institution.Descripcion,
            SitioWeb = institution.SitioWeb,
            LogoUrl = institution.LogoUrl,
            Sedes = campuses.OrderByDescending(x => x.EsPrincipal).ThenBy(x => x.Nombre).ToList(),
            Ofertas = offers,
            Contactos = contacts.OrderBy(x => x.TipoCanal).ThenBy(x => x.ValorContacto).ToList(),
            Atributos = attributes.OrderBy(x => x.Categoria).ThenBy(x => x.Nombre).ToList(),
            Multimedia = media.OrderBy(x => x.Orden).ToList()
        };
    }
}
