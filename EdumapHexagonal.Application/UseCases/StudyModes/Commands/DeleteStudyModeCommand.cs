using MediatR;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

public sealed record DeleteStudyModeCommand(short Id) : IRequest<bool>;
