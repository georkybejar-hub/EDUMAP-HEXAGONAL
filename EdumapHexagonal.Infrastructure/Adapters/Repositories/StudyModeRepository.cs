using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class StudyModeRepository : GenericRepository<ModalidadesEstudio>, IStudyModeRepository
{
    public StudyModeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
