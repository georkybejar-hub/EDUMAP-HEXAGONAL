namespace EdumapHexagonal.Domain.Constants;

public static class TiposCanal
{
    public const string Telefono = "TELEFONO";
    public const string Celular = "CELULAR";
    public const string Whatsapp = "WHATSAPP";
    public const string Email = "EMAIL";

    public static readonly IReadOnlyList<string> Todos = [Telefono, Celular, Whatsapp, Email];

    public static string Normalizar(string tipo) => tipo.Trim().ToUpperInvariant();

    public static bool EsValido(string? tipo) =>
        tipo is not null && Todos.Contains(Normalizar(tipo));

    public static bool EsTelefonico(string tipo) =>
        Normalizar(tipo) is Telefono or Celular or Whatsapp;
}
