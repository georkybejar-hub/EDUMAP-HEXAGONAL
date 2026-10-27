namespace EdumapHexagonal.Application.Common.Exceptions;


public abstract class AppException(string message) : Exception(message);

public sealed class NotFoundException(string message) : AppException(message)
{
    public NotFoundException(string entidad, object id)
        : this($"{entidad} con id '{id}' no existe.")
    {
    }
}

public sealed class ConflictException(string message) : AppException(message);

public sealed class UnauthorizedException(string message) : AppException(message);

public sealed class ForbiddenException(string message) : AppException(message);

public sealed class BusinessRuleException(string message) : AppException(message);
