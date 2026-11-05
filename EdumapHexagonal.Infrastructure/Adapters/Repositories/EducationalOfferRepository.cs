using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class EducationalOfferRepository : GenericRepository<OfertasEducativa>, IEducationalOfferRepository
{
    public EducationalOfferRepository(ApplicationDbContext context) : base(context)
    {
    }
}
