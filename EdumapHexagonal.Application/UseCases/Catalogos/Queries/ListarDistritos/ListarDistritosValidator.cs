namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarDistritos;

public sealed class ListarDistritosValidator : AbstractValidator<ListarDistritosQuery>
{
    public ListarDistritosValidator()
    {
        RuleFor(x => x.Busqueda).MaximumLength(80);
    }
}
