namespace EdumapHexagonal.Domain.Models;

public partial class Institucione
{
    public bool EstaVerificada() => Verificada == true;

    public bool EsGestionadaPor(int idUsuario) => IdRepresentante == idUsuario;

    public bool CambiaIdentificadores(string? codigoModular, string? ruc) =>
        !string.Equals(CodigoModular?.Trim(), codigoModular, StringComparison.Ordinal) ||
        !string.Equals(Ruc?.Trim(), ruc, StringComparison.Ordinal);

    public void Verificar() => Verificada = true;

    public void RevocarVerificacion() => Verificada = false;
}
