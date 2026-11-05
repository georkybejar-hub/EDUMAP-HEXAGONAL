using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Queries;

public sealed record GetAllStudyShiftsQuery : IRequest<IEnumerable<StudyShiftDto>>;
