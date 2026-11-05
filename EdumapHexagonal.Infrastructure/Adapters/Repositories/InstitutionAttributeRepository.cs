using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class InstitutionAttributeRepository : GenericRepository<InstitucionAtributo>, IInstitutionAttributeRepository
{
    public InstitutionAttributeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
