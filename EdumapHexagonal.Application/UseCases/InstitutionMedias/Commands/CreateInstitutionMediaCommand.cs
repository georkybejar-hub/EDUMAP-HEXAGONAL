using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

public sealed record CreateInstitutionMediaCommand : IRequest<InstitutionMediaDto>
{
    public int IdInstitucion { get; init; }
    public string UrlArchivo { get; init; } = string.Empty;
    public string TipoMedio { get; init; } = string.Empty;
    public string TituloImagen { get; init; } = string.Empty;
    public short? OrdenVisual { get; init; }
}
