namespace EdumapHexagonal.Application.Common.Exceptions;

// Excepciones de la capa de aplicación.
// El middleware de la API debe traducirlas a códigos HTTP:
//   NotFoundException       -> 404
//   ConflictException       -> 409
//   UnauthorizedException   -> 401
//   ForbiddenException      -> 403
//   BusinessRuleException   -> 422
//   DomainException         -> 422 (viene del dominio)
//   FluentValidation.ValidationException -> 400

public abstract class AppException(string message) : Exception(message);

/// <summary>El recurso solicitado no existe.</summary>
public sealed class NotFoundException(string message) : AppException(message)
{
    public NotFoundException(string entidad, object id)
        : this($"{entidad} con id '{id}' no existe.")
    {
    }
}

/// <summary>La operación choca con datos existentes (duplicados).</summary>
public sealed class ConflictException(string message) : AppException(message);

/// <summary>El usuario no está autenticado o sus credenciales no son válidas.</summary>
public sealed class UnauthorizedException(string message) : AppException(message);

/// <summary>El usuario está autenticado pero no tiene permiso para la operación.</summary>
public sealed class ForbiddenException(string message) : AppException(message);

/// <summary>La operación viola una regla de negocio que depende de otros datos del sistema.</summary>
public sealed class BusinessRuleException(string message) : AppException(message);
