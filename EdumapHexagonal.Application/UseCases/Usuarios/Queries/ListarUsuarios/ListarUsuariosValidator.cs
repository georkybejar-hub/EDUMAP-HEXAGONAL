namespace EdumapHexagonal.Application.UseCases.Usuarios.Queries.ListarUsuarios;

public sealed class ListarUsuariosValidator : AbstractValidator<ListarUsuariosQuery>
{
    public ListarUsuariosValidator()
    {
        RuleFor(x => x.Busqueda).MaximumLength(100);
        this.ReglasPaginacion();
    }
}
