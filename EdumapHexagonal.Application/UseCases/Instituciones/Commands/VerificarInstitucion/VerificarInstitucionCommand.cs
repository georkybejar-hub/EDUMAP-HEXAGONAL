namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.VerificarInstitucion;

/// <summary>Solo administradores. Verificada = false revoca la verificación.</summary>
public sealed record VerificarInstitucionCommand(int IdInstitucion, bool Verificada) : IRequest;
