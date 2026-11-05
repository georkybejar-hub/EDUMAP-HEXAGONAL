using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class AttributeCategoryRepository : GenericRepository<CategoriasAtributo>, IAttributeCategoryRepository
{
    public AttributeCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
