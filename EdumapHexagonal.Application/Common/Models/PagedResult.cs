namespace EdumapHexagonal.Application.Common.Models;

/// <summary>Página de resultados.</summary>
public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalRegistros, int Pagina, int TamanoPagina)
{
    public int TotalPaginas => TamanoPagina <= 0 ? 0 : (int)Math.Ceiling(TotalRegistros / (double)TamanoPagina);

    public bool TienePaginaSiguiente => Pagina < TotalPaginas;

    public PagedResult<TDestino> Map<TDestino>(Func<T, TDestino> transformar) =>
        new(Items.Select(transformar).ToList(), TotalRegistros, Pagina, TamanoPagina);
}

/// <summary>Contrato para consultas paginadas (permite reutilizar las reglas de validación).</summary>
public interface IConsultaPaginada
{
    int Pagina { get; }
    int TamanoPagina { get; }
}
