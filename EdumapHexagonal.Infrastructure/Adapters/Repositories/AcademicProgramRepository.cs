using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class AcademicProgramRepository : GenericRepository<ProgramasAcademico>, IAcademicProgramRepository
{
    public AcademicProgramRepository(ApplicationDbContext context) : base(context)
    {
    }
}
