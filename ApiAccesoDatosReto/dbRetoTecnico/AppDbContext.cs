using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiAccesoDatosReto.dbRetoTecnico;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Formasfarmaceutica> Formasfarmaceuticas { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VMedicamento> VMedicamentos { get; set; }

    public virtual DbSet<VUsuario> VUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=68.183.58.91;Initial Catalog=Cepdi_Prueba;persist security info=True;user id=user_prueba;password=Sd#s4s.S425D;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Formasfarmaceutica>(entity =>
        {
            entity.HasKey(e => e.Idformafarmaceutica).HasName("PK_FormaFarmaceutica");

            entity.ToTable("formasfarmaceuticas");

            entity.Property(e => e.Idformafarmaceutica).HasColumnName("idformafarmaceutica");
            entity.Property(e => e.Habilitado).HasColumnName("habilitado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.Idmedicamento);

            entity.ToTable("medicamentos");

            entity.Property(e => e.Idmedicamento).HasColumnName("idmedicamento");
            entity.Property(e => e.Bhabilitado).HasColumnName("bhabilitado");
            entity.Property(e => e.Concentracion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("concentracion");
            entity.Property(e => e.Idformafarmaceutica).HasColumnName("idformafarmaceutica");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Presentacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("presentacion");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdformafarmaceuticaNavigation).WithMany(p => p.Medicamentos)
                .HasForeignKey(d => d.Idformafarmaceutica)
                .HasConstraintName("FK__medicamen__idfor__15502E78");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario);

            entity.ToTable("usuarios");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Fechacreacion).HasColumnName("fechacreacion");
            entity.Property(e => e.Idperfil).HasColumnName("idperfil");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(100)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<VMedicamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_medicamentos");

            entity.Property(e => e.Concentracion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estatus)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FormasFarmaceuticas)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Presentacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VUsuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_usuarios");

            entity.Property(e => e.Estatus)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Fechacreacion).HasColumnName("fechacreacion");
            entity.Property(e => e.Idestatus).HasColumnName("idestatus");
            entity.Property(e => e.Idperfil).HasColumnName("idperfil");
            entity.Property(e => e.Idusuario)
                .ValueGeneratedOnAdd()
                .HasColumnName("idusuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
