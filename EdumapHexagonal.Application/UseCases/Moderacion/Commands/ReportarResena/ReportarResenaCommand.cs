namespace EdumapHexagonal.Application.UseCases.Moderacion.Commands.ReportarResena;

public sealed record ReportarResenaCommand(int IdResena, string MotivoReporte) : IRequest<int>;
