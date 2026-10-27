using System.Net;
using System.Text.Json;
using EdumapHexagonal.Application.Common.Exceptions;
using EdumapHexagonal.Domain.Exceptions;
using FluentValidation;

namespace EdumapHexagonal.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "OcurriÃ³ una excepciÃ³n: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode statusCode;
        string message;
        object? errors = null;

        switch (exception)
        {
            case ValidationException valEx:
                statusCode = HttpStatusCode.BadRequest;
                message = "Errores de validaciÃ³n en la solicitud.";
                errors = valEx.Errors.Select(e => new { Campo = e.PropertyName, Error = e.ErrorMessage }).ToList();
                break;
            case NotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                message = notFoundEx.Message;
                break;
            case ConflictException conflictEx:
                statusCode = HttpStatusCode.Conflict;
                message = conflictEx.Message;
                break;
            case UnauthorizedException unauthEx:
                statusCode = HttpStatusCode.Unauthorized;
                message = unauthEx.Message;
                break;
            case ForbiddenException forbiddenEx:
                statusCode = HttpStatusCode.Forbidden;
                message = forbiddenEx.Message;
                break;
            case BusinessRuleException ruleEx:
                statusCode = HttpStatusCode.UnprocessableEntity;
                message = ruleEx.Message;
                break;
            case DomainException domEx:
                statusCode = HttpStatusCode.BadRequest;
                message = domEx.Message;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "Ha ocurrido un error inesperado en el servidor.";
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            Status = (int)statusCode,
            Title = statusCode.ToString(),
            Message = message,
            Errors = errors,
            Timestamp = DateTime.UtcNow
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
