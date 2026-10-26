namespace EdumapHexagonal.Application.UseCases.Usuarios.Queries.ObtenerPerfil;

/// <summary>Perfil del usuario autenticado.</summary>
public sealed record ObtenerPerfilQuery : IRequest<PerfilDto>;
