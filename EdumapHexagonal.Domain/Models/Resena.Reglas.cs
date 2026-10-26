using EdumapHexagonal.Domain.Exceptions;

namespace EdumapHexagonal.Domain.Models;

public partial class Resena
{
    public const short PuntuacionMinima = 1;
    public const short PuntuacionMaxima = 5;
    public const int ComentarioLongitudMinima = 10;
    public const int ComentarioLongitudMaxima = 1000;

    public static Resena Crear(int idInstitucion, int idUsuario, short puntuacion, string comentario)
    {
        ValidarContenido(puntuacion, comentario);

        return new Resena
        {
            IdInstitucion = idInstitucion,
            IdUsuario = idUsuario,
            Puntuacion = puntuacion,
            Comentario = comentario.Trim(),
            Visible = true
        };
    }

    public void Editar(short puntuacion, string comentario, DateTime fecha)
    {
        if (!EstaVisible())
            throw new DomainException("No se puede editar una reseña que fue ocultada por moderación.");

        ValidarContenido(puntuacion, comentario);
        Puntuacion = puntuacion;
        Comentario = comentario.Trim();
        FechaActualizacion = fecha;
    }

    public void Ocultar() => Visible = false;

    public bool EstaVisible() => Visible != false;

    public bool EsAutor(int idUsuario) => IdUsuario == idUsuario;

    private static void ValidarContenido(short puntuacion, string comentario)
    {
        if (puntuacion is < PuntuacionMinima or > PuntuacionMaxima)
            throw new DomainException($"La puntuación debe estar entre {PuntuacionMinima} y {PuntuacionMaxima}.");

        var longitud = comentario?.Trim().Length ?? 0;
        if (longitud is < ComentarioLongitudMinima or > ComentarioLongitudMaxima)
            throw new DomainException(
                $"El comentario debe tener entre {ComentarioLongitudMinima} y {ComentarioLongitudMaxima} caracteres.");
    }
}
