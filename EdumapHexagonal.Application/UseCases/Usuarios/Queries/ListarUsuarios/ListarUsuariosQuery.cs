namespace EdumapHexagonal.Application.UseCases.Usuarios.Queries.ListarUsuarios;

public sealed record ListarUsuariosQuery(
    string? Busqueda = null,
    short? IdRol = null,
    bool? Activo = null,
    int Pagina = 1,
    int TamanoPagina = 10) : IRequest<PagedResult<UsuarioListadoDto>>, IConsultaPaginada;
