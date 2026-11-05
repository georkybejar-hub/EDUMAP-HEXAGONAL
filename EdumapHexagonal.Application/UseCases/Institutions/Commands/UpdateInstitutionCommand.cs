using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

public sealed record UpdateInstitutionCommand : IRequest<InstitutionDto?>
{
    public int IdInstitucion { get; set; }
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
    public DateTime? FechaCreacion { get; init; }
}
