using EdumapHexagonal.Application.UseCases.Sedes.Queries.ListarSedesPorInstitucion;

namespace EdumapHexagonal.Application.UseCases.Sedes.Queries.ObtenerSedePorId;

public sealed record ObtenerSedePorIdQuery(int IdSede) : IRequest<SedeDto>;
