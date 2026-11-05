using MediatR;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

public sealed record DeleteAcademicProgramCommand(int Id) : IRequest<bool>;
