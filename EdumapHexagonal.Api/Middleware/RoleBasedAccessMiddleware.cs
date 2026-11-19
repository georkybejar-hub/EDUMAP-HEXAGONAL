using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace EdumapHexagonal.Api.Middleware;

public class RoleBasedAccessMiddleware(RequestDelegate next, ILogger<RoleBasedAccessMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        var method = context.Request.Method;

        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/openapi", StringComparison.OrdinalIgnoreCase) ||
            path == "/" ||
            path.Equals("/api/auth/login", StringComparison.OrdinalIgnoreCase) ||
            path.Equals("/api/auth/register", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        if (HttpMethods.IsGet(method) && !path.StartsWith("/api/auth/me", StringComparison.OrdinalIgnoreCase)
                                      && !path.StartsWith("/api/favoritos", StringComparison.OrdinalIgnoreCase)
                                      && !path.StartsWith("/api/moderation-reports", StringComparison.OrdinalIgnoreCase)
                                      && !path.StartsWith("/api/permissions", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        var user = context.User;
        var isAuthenticated = user.Identity?.IsAuthenticated == true;

        if (!isAuthenticated)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var response = new
            {
                statusCode = (int)HttpStatusCode.Unauthorized,
                message = "Acceso no autorizado. Debe proporcionar un token JWT válido.",
                error = "Unauthorized"
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return;
        }

        var isAdminRoute = path.StartsWith("/api/moderation-reports", StringComparison.OrdinalIgnoreCase) ||
                           path.StartsWith("/api/permissions", StringComparison.OrdinalIgnoreCase);

        var isAdminMutation = !HttpMethods.IsGet(method) &&
                              !path.StartsWith("/api/favoritos", StringComparison.OrdinalIgnoreCase) &&
                              !path.StartsWith("/api/auth", StringComparison.OrdinalIgnoreCase);

        if (isAdminRoute || isAdminMutation)
        {
            var userRole = user.FindFirstValue(ClaimTypes.Role) ??
                           user.FindFirstValue("role") ??
                           (context.Request.Headers.TryGetValue("Role", out var headerRole) ? headerRole.ToString() : null);

            var isAdmin = string.Equals(userRole, "ADMINISTRADOR", StringComparison.OrdinalIgnoreCase) ||
                          string.Equals(userRole, "ADMIN", StringComparison.OrdinalIgnoreCase);

            if (!isAdmin)
            {
                logger.LogWarning("Acceso denegado a la ruta {Path} para el usuario con rol '{Role}'",
                    path, userRole ?? "sin_rol");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                var response = new
                {
                    statusCode = (int)HttpStatusCode.Forbidden,
                    message = "Acceso denegado. Se requiere rol de ADMINISTRADOR para realizar esta acción.",
                    error = "Forbidden"
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }
        }

        await next(context);
    }
}
