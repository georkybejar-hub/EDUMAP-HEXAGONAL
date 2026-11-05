using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Queries;

public sealed record GetStudyShiftByIdQuery(short Id) : IRequest<StudyShiftDto?>;
