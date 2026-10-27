namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarResenasPorInstitucion;

public sealed class ListarResenasPorInstitucionValidator : AbstractValidator<ListarResenasPorInstitucionQuery>
{
    public ListarResenasPorInstitucionValidator()
    {
        RuleFor(x => x.IdInstitucion).GreaterThan(0);
        this.ReglasPaginacion();
    }
}
