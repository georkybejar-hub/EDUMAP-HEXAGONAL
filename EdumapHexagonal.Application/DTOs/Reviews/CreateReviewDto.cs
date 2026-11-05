namespace EdumapHexagonal.Application.DTOs.Reviews;

public class CreateReviewDto
{
    public int IdInstitucion { get; set;}
    public int IdUsuario { get; set;}
    public short Puntuacion { get; set;}
    public string Comentario { get; set;} = string.Empty;
    public bool? Visible { get; set;}
    public DateTime? FechaPublicacion { get; set;}
    public DateTime? FechaActualizacion { get; set;}
}
