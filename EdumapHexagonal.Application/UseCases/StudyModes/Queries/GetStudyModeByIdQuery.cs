using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Queries;

public sealed record GetStudyModeByIdQuery(short Id) : IRequest<StudyModeDto?>;
