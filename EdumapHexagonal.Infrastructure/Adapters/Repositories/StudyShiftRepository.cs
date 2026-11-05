using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class StudyShiftRepository : GenericRepository<TurnosEstudio>, IStudyShiftRepository
{
    public StudyShiftRepository(ApplicationDbContext context) : base(context)
    {
    }
}
