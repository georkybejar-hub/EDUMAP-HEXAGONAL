namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.PublicarResena;

public sealed class PublicarResenaValidator : AbstractValidator<PublicarResenaCommand>
{
    public PublicarResenaValidator()
    {
        RuleFor(x => x.IdInstitucion).GreaterThan(0);
        RuleFor(x => x.Puntuacion).InclusiveBetween(Resena.PuntuacionMinima, Resena.PuntuacionMaxima);
        RuleFor(x => x.Comentario)
            .NotEmpty()
            .Length(Resena.ComentarioLongitudMinima, Resena.ComentarioLongitudMaxima);
    }
}
