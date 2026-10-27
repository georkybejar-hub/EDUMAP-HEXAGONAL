namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.PublicarOferta;

public sealed class PublicarOfertaHandler(
    ISedeRepository sedes,
    IProgramaAcademicoRepository programas,
    ICatalogoRepository catalogos,
    IOfertaEducativaRepository ofertas,
    ICurrentUserService usuarioActual,
    IDateTimeProvider reloj,
    IUnitOfWork unitOfWork)
    : IRequestHandler<PublicarOfertaCommand, int>
{
    public async Task<int> Handle(PublicarOfertaCommand request, CancellationToken ct)
    {
        var sede = await sedes.ObtenerPorIdAsync(request.IdSede, ct)
            ?? throw new NotFoundException("Sede", request.IdSede);

        var institucion = sede.IdInstitucionNavigation;
        usuarioActual.ValidarGestionInstitucion(institucion);

        var anioActual = reloj.Ahora.Year;
        var anioMaximo = anioActual + ReglasNegocio.AniosFuturosPermitidosOferta;
        if (request.AnioAcademico < anioActual || request.AnioAcademico > anioMaximo)
            throw new BusinessRuleException($"El aÃ±o acadÃ©mico debe estar entre {anioActual} y {anioMaximo}.");

        var programa = await programas.ObtenerPorIdAsync(request.IdPrograma, ct)
            ?? throw new NotFoundException("Programa acadÃ©mico", request.IdPrograma);

        if (programa.IdCategoria != institucion.IdCategoria)
            throw new BusinessRuleException(
                $"El programa '{programa.NombrePrograma}' no corresponde a la categorÃ­a de la instituciÃ³n.");

        if (!await catalogos.ExisteModalidadAsync(request.IdModalidad, ct))
            throw new NotFoundException("Modalidad de estudio", request.IdModalidad);

        if (!await catalogos.ExisteTurnoAsync(request.IdTurno, ct))
            throw new NotFoundException("Turno de estudio", request.IdTurno);

        if (await ofertas.ExisteCombinacionAsync(
                request.IdSede, request.IdPrograma, request.IdModalidad, request.IdTurno, request.AnioAcademico, ct))
            throw new ConflictException(
                "Ya existe una oferta para ese programa, modalidad y turno en esta sede para el mismo aÃ±o acadÃ©mico.");

        var oferta = OfertasEducativa.Crear(
            request.IdSede,
            request.IdPrograma,
            request.IdModalidad,
            request.IdTurno,
            request.AnioAcademico,
            request.CostoInscripcion,
            request.CostoMatricula,
            request.PensionMensual,
            request.CuotasAnuales);

        ofertas.Agregar(oferta);
        await unitOfWork.SaveChangesAsync(ct);

        return oferta.IdOferta;
    }
}
