namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.EditarResena;

public sealed class EditarResenaValidator : AbstractValidator<EditarResenaCommand>
{
    public EditarResenaValidator()
    {
        RuleFor(x => x.IdResena).GreaterThan(0);
        RuleFor(x => x.Puntuacion).InclusiveBetween(Resena.PuntuacionMinima, Resena.PuntuacionMaxima);
        RuleFor(x => x.Comentario)
            .NotEmpty()
            .Length(Resena.ComentarioLongitudMinima, Resena.ComentarioLongitudMaxima);
    }
}
