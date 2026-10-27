using EdumapHexagonal.Application.Ports.Out.Services;

namespace EdumapHexagonal.Infrastructure.Adapters.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Ahora => DateTime.Now;
}

