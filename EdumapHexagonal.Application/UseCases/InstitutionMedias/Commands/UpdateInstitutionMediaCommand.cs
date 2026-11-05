using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

public sealed record UpdateInstitutionMediaCommand : IRequest<InstitutionMediaDto?>
{
    public int IdMultimedia { get; set; }
    public int IdInstitucion { get; init; }
    public string UrlArchivo { get; init; } = string.Empty;
    public string TipoMedio { get; init; } = string.Empty;
    public string TituloImagen { get; init; } = string.Empty;
    public short? OrdenVisual { get; init; }
}
