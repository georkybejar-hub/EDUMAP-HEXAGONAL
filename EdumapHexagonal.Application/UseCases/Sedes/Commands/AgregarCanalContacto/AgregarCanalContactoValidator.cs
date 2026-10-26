namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.AgregarCanalContacto;

public sealed class AgregarCanalContactoValidator : AbstractValidator<AgregarCanalContactoCommand>
{
    public AgregarCanalContactoValidator()
    {
        RuleFor(x => x.IdSede).GreaterThan(0);

        RuleFor(x => x.TipoCanal)
            .NotEmpty()
            .Must(TiposCanal.EsValido)
            .WithMessage($"El tipo de canal debe ser uno de: {string.Join(", ", TiposCanal.Todos)}.");

        RuleFor(x => x.ValorContacto).NotEmpty().MaximumLength(150);

        // El formato del valor depende del tipo de canal.
        RuleFor(x => x.ValorContacto)
            .EmailAddress().WithMessage("El valor debe ser un email válido.")
            .When(x => TiposCanal.EsValido(x.TipoCanal) && TiposCanal.Normalizar(x.TipoCanal) == TiposCanal.Email);

        RuleFor(x => x.ValorContacto)
            .Matches(@"^\+?[0-9 ]{6,20}$").WithMessage("El valor debe ser un número telefónico válido (solo dígitos, espacios y + inicial).")
            .When(x => TiposCanal.EsValido(x.TipoCanal) && TiposCanal.EsTelefonico(x.TipoCanal));

        RuleFor(x => x.AreaAtencion).MaximumLength(80);
        RuleFor(x => x.HorarioAtencion).MaximumLength(100);
    }
}
