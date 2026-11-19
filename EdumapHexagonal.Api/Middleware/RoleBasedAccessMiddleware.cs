using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace EdumapHexagonal.Api.Middleware;

public class RoleBasedAccessMiddleware(RequestDelegate next, ILogger<RoleBasedAccessMiddleware> logger)
{
    private static readonly HashSet<string> PublicGetPrefixes = new(StringComparer.OrdinalIgnoreCase)
    {
        "/api/institutions",
        "/api/institution-categories",
        "/api/institution-medias",
        "/api/institution-attributes",
        "/api/institution-search-views",
        "/api/campuses",
        "/api/districts",
        "/api/academic-programs",
        "/api/educational-offers",
        "/api/contact-channels",
        "/api/catalog-attributes",
        "/api/attribute-categories",
        "/api/management-types",
        "/api/regulatory-entities",
        "/api/study-modes",
        "/api/study-shifts",
        "/api/reviews"
    };

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        var method = context.Request.Method;

        // 1. Rutas públicas de Swagger / OpenAPI y autenticación
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/openapi", StringComparison.OrdinalIgnoreCase) ||
            path == "/" ||
            path.Equals("/api/auth/login", StringComparison.OrdinalIgnoreCase) ||
            path.Equals("/api/auth/register", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        // 2. Consultas GET a catálogos públicos (no requieren autenticación)
        if (HttpMethods.IsGet(method) && IsPublicGet(path))
        {
            await next(context);
            return;
        }

        // 3. Cualquier otra ruta requiere autenticación válida mediante JWT
        var user = context.User;
        var isAuthenticated = user.Identity?.IsAuthenticated == true;

        if (!isAuthenticated)
        {
            await WriteJsonErrorAsync(context, HttpStatusCode.Unauthorized,
                "Acceso no autorizado. Debe proporcionar un token JWT válido.", "Unauthorized");
            return;
        }

        // 4. Verificación de permisos para operaciones de ADMINISTRADOR
        // Rutas de administración interna que requieren rol ADMINISTRADOR
        var isAdminOnlyRoute = path.StartsWith("/api/users", StringComparison.OrdinalIgnoreCase) ||
                               path.StartsWith("/api/roles", StringComparison.OrdinalIgnoreCase) ||
                               path.StartsWith("/api/role-permissions", StringComparison.OrdinalIgnoreCase) ||
                               path.StartsWith("/api/permissions", StringComparison.OrdinalIgnoreCase) ||
                               (path.StartsWith("/api/moderation-reports", StringComparison.OrdinalIgnoreCase) && !HttpMethods.IsPost(method));

        // Mutaciones permitidas para usuarios autenticados comunes (POST en reseñas, reportes de moderación y sus propios favoritos)
        var isUserAllowedMutation = path.StartsWith("/api/favoritos", StringComparison.OrdinalIgnoreCase) ||
                                    (HttpMethods.IsPost(method) && path.Contains("/reviews", StringComparison.OrdinalIgnoreCase)) ||
                                    (HttpMethods.IsPost(method) && path.StartsWith("/api/moderation-reports", StringComparison.OrdinalIgnoreCase));

        var requiresAdmin = isAdminOnlyRoute || (!HttpMethods.IsGet(method) && !isUserAllowedMutation);

        if (requiresAdmin)
        {
            // Solo claims validados criptográficamente en el JWT, NUNCA headers HTTP manipulables por el cliente
            var userRole = user.FindFirstValue(ClaimTypes.Role) ?? user.FindFirstValue("role");

            var isAdmin = string.Equals(userRole, "ADMINISTRADOR", StringComparison.OrdinalIgnoreCase) ||
                          string.Equals(userRole, "ADMIN", StringComparison.OrdinalIgnoreCase);

            if (!isAdmin)
            {
                logger.LogWarning("Acceso denegado a la ruta {Path} para el usuario con rol '{Role}'",
                    path, userRole ?? "sin_rol");

                await WriteJsonErrorAsync(context, HttpStatusCode.Forbidden,
                    "Acceso denegado. Se requiere rol de ADMINISTRADOR para realizar esta acción.", "Forbidden");
                return;
            }
        }

        await next(context);
    }

    private static bool IsPublicGet(string path)
    {
        foreach (var prefix in PublicGetPrefixes)
        {
            if (path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    private static async Task WriteJsonErrorAsync(HttpContext context, HttpStatusCode statusCode, string message, string error)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        var response = new
        {
            statusCode = (int)statusCode,
            message,
            error
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
