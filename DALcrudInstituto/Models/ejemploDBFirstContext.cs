using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DALcrudInstituto.Models
{
    public partial class ejemploDBFirstContext : DbContext
    {
        public ejemploDBFirstContext()
        {
        }

        public ejemploDBFirstContext(DbContextOptions<ejemploDBFirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlumnHasAsig> AlumnHasAsigs { get; set; } = null!;
        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Asignatura> Asignaturas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ejemploDBFirst;User Id=postgres;Password=root123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlumnHasAsig>(entity =>
            {
                entity.HasKey(e => e.IdRel)
                    .HasName("alumn_has_asig_pkey");

                entity.ToTable("alumn_has_asig");

                entity.HasComment("Tabla relacional entre alumno y asignaturas");

                entity.Property(e => e.IdRel).HasColumnName("idRel");

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.AlumnHasAsigs)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_alum_fk");

                entity.HasOne(d => d.IdAsignaturaNavigation)
                    .WithMany(p => p.AlumnHasAsigs)
                    .HasForeignKey(d => d.IdAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_asign_fk");
            });

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("alumno_pkey");

                entity.ToTable("alumnos");

                entity.Property(e => e.IdAlumno)
                    .HasColumnName("idAlumno")
                    .HasDefaultValueSql("nextval('\"alumno_idAlumno_seq\"'::regclass)");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(300)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(300)
                    .HasColumnName("direccion");

                entity.Property(e => e.Mail)
                    .HasMaxLength(300)
                    .HasColumnName("mail");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.HasKey(e => e.IdAsignatura)
                    .HasName("asignaturas_pkey");

                entity.ToTable("asignaturas");

                entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
