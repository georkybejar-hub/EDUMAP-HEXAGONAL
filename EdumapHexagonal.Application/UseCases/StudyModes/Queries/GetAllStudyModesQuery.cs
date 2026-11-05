using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Queries;

public sealed record GetAllStudyModesQuery : IRequest<IEnumerable<StudyModeDto>>;
