using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class ContactChannelRepository : GenericRepository<CanalesContactoSede>, IContactChannelRepository
{
    public ContactChannelRepository(ApplicationDbContext context) : base(context)
    {
    }
}
