namespace EdumapHexagonal.Domain.Constants;

public static class TiposMedio
{
    public const string Imagen = "IMAGEN";
    public const string Video = "VIDEO";

    public static readonly IReadOnlyList<string> Todos = [Imagen, Video];

    public static string Normalizar(string tipo) => tipo.Trim().ToUpperInvariant();

    public static bool EsValido(string? tipo) =>
        tipo is not null && Todos.Contains(Normalizar(tipo));
}
