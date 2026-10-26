namespace EdumapHexagonal.Application.Common.Security;

/// <summary>
/// Nombres de los roles (tabla roles.nombre_rol). Se comparan sin distinguir mayúsculas.
/// IMPORTANTE: ajústalos a los valores reales que tienes en tu base de datos.
/// </summary>
public static class RolesSistema
{
    public const string Administrador = "ADMINISTRADOR";
    public const string Representante = "REPRESENTANTE";
    public const string Usuario = "USUARIO";

    /// <summary>Rol que se asigna a quien se registra desde la API pública.</summary>
    public const string PorDefectoAlRegistrar = Usuario;
}
