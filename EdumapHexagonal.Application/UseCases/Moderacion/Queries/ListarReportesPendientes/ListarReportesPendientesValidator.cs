namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ListarReportesPendientes;

public sealed class ListarReportesPendientesValidator : AbstractValidator<ListarReportesPendientesQuery>
{
    public ListarReportesPendientesValidator()
    {
        this.ReglasPaginacion();
    }
}
