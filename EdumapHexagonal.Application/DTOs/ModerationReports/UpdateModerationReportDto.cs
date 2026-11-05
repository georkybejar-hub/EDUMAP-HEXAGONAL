namespace EdumapHexagonal.Application.DTOs.ModerationReports;

public class UpdateModerationReportDto
{
    public int IdResena { get; set;}
    public int IdUsuarioReporta { get; set;}
    public int? IdAdminRevisor { get; set;}
    public string MotivoReporte { get; set;} = string.Empty;
    public string EstadoRevision { get; set;} = string.Empty;
    public string? ResolucionAdmin { get; set;}
    public DateTime? FechaReporte { get; set;}
    public DateTime? FechaResolucion { get; set;}
}
