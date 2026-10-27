using System;
using System.Collections.Generic;
using EdumapHexagonal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AtributosCatalogo> AtributosCatalogos { get; set; }

    public virtual DbSet<CanalesContactoSede> CanalesContactoSedes { get; set; }

    public virtual DbSet<CategoriasAtributo> CategoriasAtributos { get; set; }

    public virtual DbSet<CategoriasInstitucion> CategoriasInstitucions { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<EntidadesReguladora> EntidadesReguladoras { get; set; }

    public virtual DbSet<FavoritosComparador> FavoritosComparadors { get; set; }

    public virtual DbSet<InstitucionAtributo> InstitucionAtributos { get; set; }

    public virtual DbSet<Institucione> Instituciones { get; set; }

    public virtual DbSet<ModalidadesEstudio> ModalidadesEstudios { get; set; }

    public virtual DbSet<MultimediaInstitucion> MultimediaInstitucions { get; set; }

    public virtual DbSet<OfertasEducativa> OfertasEducativas { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<ProgramasAcademico> ProgramasAcademicos { get; set; }

    public virtual DbSet<ReportesModeracion> ReportesModeracions { get; set; }

    public virtual DbSet<Resena> Resenas { get; set; }

    public virtual DbSet<RolPermiso> RolPermisos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SedesInstitucion> SedesInstitucions { get; set; }

    public virtual DbSet<TiposGestion> TiposGestions { get; set; }

    public virtual DbSet<TurnosEstudio> TurnosEstudios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VwBuscadorInstitucione> VwBuscadorInstituciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AtributosCatalogo>(entity =>
        {
            entity.HasKey(e => e.IdAtributo).HasName("atributos_catalogo_pkey");

            entity.ToTable("atributos_catalogo");

            entity.HasIndex(e => e.NombreAtributo, "atributos_catalogo_nombre_atributo_key").IsUnique();

            entity.Property(e => e.IdAtributo).HasColumnName("id_atributo");
            entity.Property(e => e.IdCatAtributo).HasColumnName("id_cat_atributo");
            entity.Property(e => e.NombreAtributo)
                .HasMaxLength(100)
                .HasColumnName("nombre_atributo");

            entity.HasOne(d => d.IdCatAtributoNavigation).WithMany(p => p.AtributosCatalogos)
                .HasForeignKey(d => d.IdCatAtributo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("atributos_catalogo_id_cat_atributo_fkey");
        });

        modelBuilder.Entity<CanalesContactoSede>(entity =>
        {
            entity.HasKey(e => e.IdContacto).HasName("canales_contacto_sede_pkey");

            entity.ToTable("canales_contacto_sede");

            entity.Property(e => e.IdContacto).HasColumnName("id_contacto");
            entity.Property(e => e.AreaAtencion)
                .HasMaxLength(80)
                .HasDefaultValueSql("'Admisión e Informes'::character varying")
                .HasColumnName("area_atencion");
            entity.Property(e => e.HorarioAtencion)
                .HasMaxLength(100)
                .HasColumnName("horario_atencion");
            entity.Property(e => e.IdSede).HasColumnName("id_sede");
            entity.Property(e => e.TipoCanal)
                .HasMaxLength(30)
                .HasColumnName("tipo_canal");
            entity.Property(e => e.ValorContacto)
                .HasMaxLength(150)
                .HasColumnName("valor_contacto");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.CanalesContactoSedes)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("canales_contacto_sede_id_sede_fkey");
        });

        modelBuilder.Entity<CategoriasAtributo>(entity =>
        {
            entity.HasKey(e => e.IdCatAtributo).HasName("categorias_atributo_pkey");

            entity.ToTable("categorias_atributo");

            entity.HasIndex(e => e.NombreCatAtributo, "categorias_atributo_nombre_cat_atributo_key").IsUnique();

            entity.Property(e => e.IdCatAtributo).HasColumnName("id_cat_atributo");
            entity.Property(e => e.IconoUi)
                .HasMaxLength(50)
                .HasColumnName("icono_ui");
            entity.Property(e => e.NombreCatAtributo)
                .HasMaxLength(80)
                .HasColumnName("nombre_cat_atributo");
        });

        modelBuilder.Entity<CategoriasInstitucion>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("categorias_institucion_pkey");

            entity.ToTable("categorias_institucion");

            entity.HasIndex(e => e.NombreCategoria, "categorias_institucion_nombre_categoria_key").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.NivelEducativo)
                .HasMaxLength(80)
                .HasColumnName("nivel_educativo");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("distritos_pkey");

            entity.ToTable("distritos");

            entity.HasIndex(e => e.NombreDistrito, "distritos_nombre_distrito_key").IsUnique();

            entity.HasIndex(e => e.Ubigeo, "distritos_ubigeo_key").IsUnique();

            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.LatitudCentro)
                .HasPrecision(10, 8)
                .HasColumnName("latitud_centro");
            entity.Property(e => e.LongitudCentro)
                .HasPrecision(11, 8)
                .HasColumnName("longitud_centro");
            entity.Property(e => e.NombreDistrito)
                .HasMaxLength(80)
                .HasColumnName("nombre_distrito");
            entity.Property(e => e.Ubigeo)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("ubigeo");
        });

        modelBuilder.Entity<EntidadesReguladora>(entity =>
        {
            entity.HasKey(e => e.IdReguladora).HasName("entidades_reguladoras_pkey");

            entity.ToTable("entidades_reguladoras");

            entity.HasIndex(e => e.Siglas, "entidades_reguladoras_siglas_key").IsUnique();

            entity.Property(e => e.IdReguladora).HasColumnName("id_reguladora");
            entity.Property(e => e.NombreEntidad)
                .HasMaxLength(150)
                .HasColumnName("nombre_entidad");
            entity.Property(e => e.Siglas)
                .HasMaxLength(20)
                .HasColumnName("siglas");
        });

        modelBuilder.Entity<FavoritosComparador>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdInstitucion }).HasName("favoritos_comparador_pkey");

            entity.ToTable("favoritos_comparador");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.FechaAgregado)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_agregado");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.FavoritosComparadors)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("favoritos_comparador_id_institucion_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.FavoritosComparadors)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("favoritos_comparador_id_usuario_fkey");
        });

        modelBuilder.Entity<InstitucionAtributo>(entity =>
        {
            entity.HasKey(e => new { e.IdInstitucion, e.IdAtributo }).HasName("institucion_atributos_pkey");

            entity.ToTable("institucion_atributos");

            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.IdAtributo).HasColumnName("id_atributo");
            entity.Property(e => e.DetalleValor)
                .HasMaxLength(150)
                .HasColumnName("detalle_valor");

            entity.HasOne(d => d.IdAtributoNavigation).WithMany(p => p.InstitucionAtributos)
                .HasForeignKey(d => d.IdAtributo)
                .HasConstraintName("institucion_atributos_id_atributo_fkey");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.InstitucionAtributos)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("institucion_atributos_id_institucion_fkey");
        });

        modelBuilder.Entity<Institucione>(entity =>
        {
            entity.HasKey(e => e.IdInstitucion).HasName("instituciones_pkey");

            entity.ToTable("instituciones");

            entity.HasIndex(e => new { e.IdCategoria, e.IdTipoGestion, e.Verificada }, "idx_inst_filtros");

            entity.HasIndex(e => e.CodigoModular, "instituciones_codigo_modular_key").IsUnique();

            entity.HasIndex(e => e.Ruc, "instituciones_ruc_key").IsUnique();

            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.CodigoModular)
                .HasMaxLength(25)
                .HasColumnName("codigo_modular");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdReguladora).HasColumnName("id_reguladora");
            entity.Property(e => e.IdRepresentante).HasColumnName("id_representante");
            entity.Property(e => e.IdTipoGestion).HasColumnName("id_tipo_gestion");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(255)
                .HasColumnName("logo_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Ruc)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("ruc");
            entity.Property(e => e.SitioWeb)
                .HasMaxLength(255)
                .HasColumnName("sitio_web");
            entity.Property(e => e.Verificada)
                .HasDefaultValue(false)
                .HasColumnName("verificada");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Instituciones)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("instituciones_id_categoria_fkey");

            entity.HasOne(d => d.IdReguladoraNavigation).WithMany(p => p.Instituciones)
                .HasForeignKey(d => d.IdReguladora)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("instituciones_id_reguladora_fkey");

            entity.HasOne(d => d.IdRepresentanteNavigation).WithMany(p => p.Instituciones)
                .HasForeignKey(d => d.IdRepresentante)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("instituciones_id_representante_fkey");

            entity.HasOne(d => d.IdTipoGestionNavigation).WithMany(p => p.Instituciones)
                .HasForeignKey(d => d.IdTipoGestion)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("instituciones_id_tipo_gestion_fkey");
        });

        modelBuilder.Entity<ModalidadesEstudio>(entity =>
        {
            entity.HasKey(e => e.IdModalidad).HasName("modalidades_estudio_pkey");

            entity.ToTable("modalidades_estudio");

            entity.HasIndex(e => e.NombreModalidad, "modalidades_estudio_nombre_modalidad_key").IsUnique();

            entity.Property(e => e.IdModalidad).HasColumnName("id_modalidad");
            entity.Property(e => e.NombreModalidad)
                .HasMaxLength(50)
                .HasColumnName("nombre_modalidad");
        });

        modelBuilder.Entity<MultimediaInstitucion>(entity =>
        {
            entity.HasKey(e => e.IdMultimedia).HasName("multimedia_institucion_pkey");

            entity.ToTable("multimedia_institucion");

            entity.Property(e => e.IdMultimedia).HasColumnName("id_multimedia");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.OrdenVisual)
                .HasDefaultValue((short)1)
                .HasColumnName("orden_visual");
            entity.Property(e => e.TipoMedio)
                .HasMaxLength(20)
                .HasColumnName("tipo_medio");
            entity.Property(e => e.TituloImagen)
                .HasMaxLength(100)
                .HasColumnName("titulo_imagen");
            entity.Property(e => e.UrlArchivo)
                .HasMaxLength(255)
                .HasColumnName("url_archivo");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.MultimediaInstitucions)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("multimedia_institucion_id_institucion_fkey");
        });

        modelBuilder.Entity<OfertasEducativa>(entity =>
        {
            entity.HasKey(e => e.IdOferta).HasName("ofertas_educativas_pkey");

            entity.ToTable("ofertas_educativas");

            entity.HasIndex(e => new { e.PensionMensual, e.IdModalidad, e.IdPrograma }, "idx_ofertas_costos");

            entity.HasIndex(e => new { e.IdSede, e.IdPrograma, e.IdModalidad, e.IdTurno, e.AnioAcademico }, "uq_oferta_sede_prog_mod_turno").IsUnique();

            entity.Property(e => e.IdOferta).HasColumnName("id_oferta");
            entity.Property(e => e.AnioAcademico)
                .HasDefaultValueSql("EXTRACT(year FROM CURRENT_DATE)")
                .HasColumnName("anio_academico");
            entity.Property(e => e.CostoInscripcion)
                .HasPrecision(10, 2)
                .HasColumnName("costo_inscripcion");
            entity.Property(e => e.CostoMatricula)
                .HasPrecision(10, 2)
                .HasColumnName("costo_matricula");
            entity.Property(e => e.CuotasAnuales)
                .HasDefaultValue((short)10)
                .HasColumnName("cuotas_anuales");
            entity.Property(e => e.IdModalidad).HasColumnName("id_modalidad");
            entity.Property(e => e.IdPrograma).HasColumnName("id_programa");
            entity.Property(e => e.IdSede).HasColumnName("id_sede");
            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.PensionMensual)
                .HasPrecision(10, 2)
                .HasColumnName("pension_mensual");

            entity.HasOne(d => d.IdModalidadNavigation).WithMany(p => p.OfertasEducativas)
                .HasForeignKey(d => d.IdModalidad)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ofertas_educativas_id_modalidad_fkey");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.OfertasEducativas)
                .HasForeignKey(d => d.IdPrograma)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ofertas_educativas_id_programa_fkey");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.OfertasEducativas)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("ofertas_educativas_id_sede_fkey");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.OfertasEducativas)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ofertas_educativas_id_turno_fkey");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("permisos_pkey");

            entity.ToTable("permisos");

            entity.HasIndex(e => e.CodigoPermiso, "permisos_codigo_permiso_key").IsUnique();

            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.CodigoPermiso)
                .HasMaxLength(60)
                .HasColumnName("codigo_permiso");
            entity.Property(e => e.ModuloSistema)
                .HasMaxLength(50)
                .HasColumnName("modulo_sistema");
        });

        modelBuilder.Entity<ProgramasAcademico>(entity =>
        {
            entity.HasKey(e => e.IdPrograma).HasName("programas_academicos_pkey");

            entity.ToTable("programas_academicos");

            entity.HasIndex(e => new { e.IdCategoria, e.NombrePrograma }, "uq_programa_categoria").IsUnique();

            entity.Property(e => e.IdPrograma).HasColumnName("id_programa");
            entity.Property(e => e.AreaAcademica)
                .HasMaxLength(100)
                .HasColumnName("area_academica");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.NombrePrograma)
                .HasMaxLength(150)
                .HasColumnName("nombre_programa");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.ProgramasAcademicos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("programas_academicos_id_categoria_fkey");
        });

        modelBuilder.Entity<ReportesModeracion>(entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("reportes_moderacion_pkey");

            entity.ToTable("reportes_moderacion");

            entity.Property(e => e.IdReporte).HasColumnName("id_reporte");
            entity.Property(e => e.EstadoRevision)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado_revision");
            entity.Property(e => e.FechaReporte)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_reporte");
            entity.Property(e => e.FechaResolucion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_resolucion");
            entity.Property(e => e.IdAdminRevisor).HasColumnName("id_admin_revisor");
            entity.Property(e => e.IdResena).HasColumnName("id_resena");
            entity.Property(e => e.IdUsuarioReporta).HasColumnName("id_usuario_reporta");
            entity.Property(e => e.MotivoReporte)
                .HasMaxLength(150)
                .HasColumnName("motivo_reporte");
            entity.Property(e => e.ResolucionAdmin).HasColumnName("resolucion_admin");

            entity.HasOne(d => d.IdAdminRevisorNavigation).WithMany(p => p.ReportesModeracionIdAdminRevisorNavigations)
                .HasForeignKey(d => d.IdAdminRevisor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("reportes_moderacion_id_admin_revisor_fkey");

            entity.HasOne(d => d.IdResenaNavigation).WithMany(p => p.ReportesModeracions)
                .HasForeignKey(d => d.IdResena)
                .HasConstraintName("reportes_moderacion_id_resena_fkey");

            entity.HasOne(d => d.IdUsuarioReportaNavigation).WithMany(p => p.ReportesModeracionIdUsuarioReportaNavigations)
                .HasForeignKey(d => d.IdUsuarioReporta)
                .HasConstraintName("reportes_moderacion_id_usuario_reporta_fkey");
        });

        modelBuilder.Entity<Resena>(entity =>
        {
            entity.HasKey(e => e.IdResena).HasName("resenas_pkey");

            entity.ToTable("resenas");

            entity.HasIndex(e => new { e.IdInstitucion, e.Puntuacion }, "idx_resenas_visibles").HasFilter("(visible = true)");

            entity.HasIndex(e => new { e.IdInstitucion, e.IdUsuario }, "uq_resena_usuario_institucion").IsUnique();

            entity.Property(e => e.IdResena).HasColumnName("id_resena");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_publicacion");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");
            entity.Property(e => e.Visible)
                .HasDefaultValue(true)
                .HasColumnName("visible");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("resenas_id_institucion_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("resenas_id_usuario_fkey");
        });

        modelBuilder.Entity<RolPermiso>(entity =>
        {
            entity.HasKey(e => new { e.IdRol, e.IdPermiso }).HasName("rol_permisos_pkey");

            entity.ToTable("rol_permisos");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("rol_permisos_id_permiso_fkey");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("rol_permisos_id_rol_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.NombreRol, "roles_nombre_rol_key").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.DescripcionRol)
                .HasMaxLength(200)
                .HasColumnName("descripcion_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<SedesInstitucion>(entity =>
        {
            entity.HasKey(e => e.IdSede).HasName("sedes_institucion_pkey");

            entity.ToTable("sedes_institucion");

            entity.HasIndex(e => new { e.IdDistrito, e.Latitud, e.Longitud }, "idx_sedes_ubicacion");

            entity.Property(e => e.IdSede).HasColumnName("id_sede");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.EsPrincipal)
                .HasDefaultValue(true)
                .HasColumnName("es_principal");
            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.Latitud)
                .HasPrecision(10, 8)
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasPrecision(11, 8)
                .HasColumnName("longitud");
            entity.Property(e => e.NombreSede)
                .HasMaxLength(100)
                .HasDefaultValueSql("'Sede Principal'::character varying")
                .HasColumnName("nombre_sede");
            entity.Property(e => e.Referencia)
                .HasMaxLength(200)
                .HasColumnName("referencia");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.SedesInstitucions)
                .HasForeignKey(d => d.IdDistrito)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sedes_institucion_id_distrito_fkey");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.SedesInstitucions)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("sedes_institucion_id_institucion_fkey");
        });

        modelBuilder.Entity<TiposGestion>(entity =>
        {
            entity.HasKey(e => e.IdTipoGestion).HasName("tipos_gestion_pkey");

            entity.ToTable("tipos_gestion");

            entity.HasIndex(e => e.NombreGestion, "tipos_gestion_nombre_gestion_key").IsUnique();

            entity.Property(e => e.IdTipoGestion).HasColumnName("id_tipo_gestion");
            entity.Property(e => e.NombreGestion)
                .HasMaxLength(50)
                .HasColumnName("nombre_gestion");
        });

        modelBuilder.Entity<TurnosEstudio>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("turnos_estudio_pkey");

            entity.ToTable("turnos_estudio");

            entity.HasIndex(e => e.NombreTurno, "turnos_estudio_nombre_turno_key").IsUnique();

            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.NombreTurno)
                .HasMaxLength(50)
                .HasColumnName("nombre_turno");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "usuarios_email_key").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuarios_id_rol_fkey");
        });

        modelBuilder.Entity<VwBuscadorInstitucione>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_buscador_instituciones");

            entity.Property(e => e.CalificacionPromedio).HasColumnName("calificacion_promedio");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.EnteRegulador)
                .HasMaxLength(20)
                .HasColumnName("ente_regulador");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.IdSedePrincipal).HasColumnName("id_sede_principal");
            entity.Property(e => e.IdTipoGestion).HasColumnName("id_tipo_gestion");
            entity.Property(e => e.Latitud)
                .HasPrecision(10, 8)
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasPrecision(11, 8)
                .HasColumnName("longitud");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .HasColumnName("nombre_categoria");
            entity.Property(e => e.NombreDistrito)
                .HasMaxLength(80)
                .HasColumnName("nombre_distrito");
            entity.Property(e => e.NombreGestion)
                .HasMaxLength(50)
                .HasColumnName("nombre_gestion");
            entity.Property(e => e.PensionMaxima).HasColumnName("pension_maxima");
            entity.Property(e => e.PensionMinima).HasColumnName("pension_minima");
            entity.Property(e => e.TotalGuardadosComparador).HasColumnName("total_guardados_comparador");
            entity.Property(e => e.TotalResenas).HasColumnName("total_resenas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
