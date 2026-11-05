using MediatR;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

public sealed record DeleteStudyShiftCommand(short Id) : IRequest<bool>;
