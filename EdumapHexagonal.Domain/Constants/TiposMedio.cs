namespace EdumapHexagonal.Domain.Constants;

/// <summary>
/// Tipos de archivo multimedia de una institución (columna multimedia_institucion.tipo_medio).
/// </summary>
public static class TiposMedio
{
    public const string Imagen = "IMAGEN";
    public const string Video = "VIDEO";

    public static readonly IReadOnlyList<string> Todos = [Imagen, Video];

    public static string Normalizar(string tipo) => tipo.Trim().ToUpperInvariant();

    public static bool EsValido(string? tipo) =>
        tipo is not null && Todos.Contains(Normalizar(tipo));
}
