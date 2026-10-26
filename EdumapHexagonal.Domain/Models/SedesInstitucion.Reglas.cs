using EdumapHexagonal.Domain.Constants;
using EdumapHexagonal.Domain.Exceptions;

namespace EdumapHexagonal.Domain.Models;

public partial class SedesInstitucion
{
    public const string AreaAtencionPorDefecto = "Admisión e Informes";

    public bool EsSedePrincipal() => EsPrincipal == true;

    public void MarcarComoPrincipal() => EsPrincipal = true;

    public void DesmarcarComoPrincipal() => EsPrincipal = false;

    public void CambiarUbicacion(short idDistrito, string direccion, string? referencia, decimal latitud, decimal longitud)
    {
        if (!LimitesPeru.ContieneCoordenadas(latitud, longitud))
            throw new DomainException("Las coordenadas de la sede deben estar dentro del territorio peruano.");

        IdDistrito = idDistrito;
        Direccion = direccion.Trim();
        Referencia = string.IsNullOrWhiteSpace(referencia) ? null : referencia.Trim();
        Latitud = latitud;
        Longitud = longitud;
    }
}
