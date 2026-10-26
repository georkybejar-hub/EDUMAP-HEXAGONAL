namespace EdumapHexagonal.Domain.Models;

// Reglas de negocio de Institucione.
// Van en un archivo partial aparte para que no se pierdan al volver a generar los modelos desde la BD.
public partial class Institucione
{
    public bool EstaVerificada() => Verificada == true;

    public bool EsGestionadaPor(int idUsuario) => IdRepresentante == idUsuario;

    /// <summary>Indica si los identificadores oficiales (código modular o RUC) cambiarían.</summary>
    public bool CambiaIdentificadores(string? codigoModular, string? ruc) =>
        !string.Equals(CodigoModular?.Trim(), codigoModular, StringComparison.Ordinal) ||
        !string.Equals(Ruc?.Trim(), ruc, StringComparison.Ordinal);

    public void Verificar() => Verificada = true;

    public void RevocarVerificacion() => Verificada = false;
}
