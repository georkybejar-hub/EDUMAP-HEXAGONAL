using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Queries;

public sealed record GetAcademicProgramByIdQuery(int Id) : IRequest<AcademicProgramDto?>;
