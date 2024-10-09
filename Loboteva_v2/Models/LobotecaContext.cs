using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Loboteva_v2.Models
{
    public partial class LobotecaContext : DbContext
    {
        public LobotecaContext()
        {
        }

        public LobotecaContext(DbContextOptions<LobotecaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradors { get; set; } = null!;
        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Autor> Autors { get; set; } = null!;
        public virtual DbSet<AutorELibro> AutorELibros { get; set; } = null!;
        public virtual DbSet<AutorLibro> AutorLibros { get; set; } = null!;
        public virtual DbSet<AutorRevistum> AutorRevista { get; set; } = null!;
        public virtual DbSet<Carrera> Carreras { get; set; } = null!;
        public virtual DbSet<ConfiguracionPenalizacione> ConfiguracionPenalizaciones { get; set; } = null!;
        public virtual DbSet<ELibro> ELibros { get; set; } = null!;
        public virtual DbSet<Editorial> Editorials { get; set; } = null!;
        public virtual DbSet<Libro> Libros { get; set; } = null!;
        public virtual DbSet<Prestamo> Prestamos { get; set; } = null!;
        public virtual DbSet<Revistum> Revista { get; set; } = null!;
        public virtual DbSet<Sancione> Sanciones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.ToTable("administrador");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido_materno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido_paterno");

                entity.Property(e => e.FechaDeInicio)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_inicio");

                entity.Property(e => e.FechaDeTermino)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_termino");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumeroDeEmpleado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numero_de_empleado");
            });

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("alumno");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido_materno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido_paterno");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdCarrera).HasColumnName("id_carrera");

                entity.Property(e => e.Matricula)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("matricula");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.oCarrera)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdCarrera)
                    .HasConstraintName("FK__alumno__id_carre__398D8EEE");
            });

            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("autor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido_materno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido_paterno");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<AutorELibro>(entity =>
            {
                entity.ToTable("autor_e_libro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAutor).HasColumnName("id_autor");

                entity.Property(e => e.IdELibro).HasColumnName("id_e_libro");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.AutorELibros)
                    .HasForeignKey(d => d.IdAutor)
                    .HasConstraintName("FK__autor_e_l__id_au__5629CD9C");

                entity.HasOne(d => d.IdELibroNavigation)
                    .WithMany(p => p.AutorELibros)
                    .HasForeignKey(d => d.IdELibro)
                    .HasConstraintName("FK__autor_e_l__id_e___571DF1D5");
            });

            modelBuilder.Entity<AutorLibro>(entity =>
            {
                entity.ToTable("autor_libro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAutor).HasColumnName("id_autor");

                entity.Property(e => e.IdLibro).HasColumnName("id_libro");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.AutorLibros)
                    .HasForeignKey(d => d.IdAutor)
                    .HasConstraintName("FK__autor_lib__id_au__4F7CD00D");

                entity.HasOne(d => d.IdLibroNavigation)
                    .WithMany(p => p.AutorLibros)
                    .HasForeignKey(d => d.IdLibro)
                    .HasConstraintName("FK__autor_lib__id_li__5070F446");
            });

            modelBuilder.Entity<AutorRevistum>(entity =>
            {
                entity.ToTable("autor_revista");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAutor).HasColumnName("id_autor");

                entity.Property(e => e.IdRevista).HasColumnName("id_revista");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.AutorRevista)
                    .HasForeignKey(d => d.IdAutor)
                    .HasConstraintName("FK__autor_rev__id_au__5CD6CB2B");

                entity.HasOne(d => d.IdRevistaNavigation)
                    .WithMany(p => p.AutorRevista)
                    .HasForeignKey(d => d.IdRevista)
                    .HasConstraintName("FK__autor_rev__id_re__5DCAEF64");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.ToTable("carrera");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.NombreDeLaCarrera)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_de_la_carrera");
            });

            modelBuilder.Entity<ConfiguracionPenalizacione>(entity =>
            {
                entity.HasKey(e => e.IdPenalizacion)
                    .HasName("PK__configur__E2D118F21727510D");

                entity.ToTable("configuracion_penalizaciones");

                entity.Property(e => e.IdPenalizacion).HasColumnName("id_penalizacion");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("monto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<ELibro>(entity =>
            {
                entity.ToTable("e_libro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Archivo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("archivo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaDeAlta)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_alta");

                entity.Property(e => e.FechaDePublicacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_publicacion");

                entity.Property(e => e.Genero)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ruta_imagen");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.IdEditorialNavigation)
                    .WithMany(p => p.ELibros)
                    .HasForeignKey(d => d.IdEditorial)
                    .HasConstraintName("FK__e_libro__id_edit__534D60F1");
            });

            modelBuilder.Entity<Editorial>(entity =>
            {
                entity.ToTable("editorial");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.ToTable("libro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaDeAlta)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_alta");

                entity.Property(e => e.FechaDePublicacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_publicacion");

                entity.Property(e => e.Genero)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ruta_imagen");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.IdEditorialNavigation)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.IdEditorial)
                    .HasConstraintName("FK__libro__id_editor__440B1D61");
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.ToTable("prestamo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaDeDevolucion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_devolucion");

                entity.Property(e => e.FechaDePrestamo)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_prestamo");

                entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");

                entity.Property(e => e.IdLibro).HasColumnName("id_libro");

                entity.HasOne(d => d.IdAdministradorNavigation)
                    .WithMany(p => p.Prestamos)
                    .HasForeignKey(d => d.IdAdministrador)
                    .HasConstraintName("FK__prestamo__id_adm__46E78A0C");

                entity.HasOne(d => d.IdLibroNavigation)
                    .WithMany(p => p.Prestamos)
                    .HasForeignKey(d => d.IdLibro)
                    .HasConstraintName("FK__prestamo__id_lib__47DBAE45");
            });

            modelBuilder.Entity<Revistum>(entity =>
            {
                entity.ToTable("revista");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Archivo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("archivo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaDeAlta)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_alta");

                entity.Property(e => e.FechaDePublicacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_de_publicacion");

                entity.Property(e => e.Genero)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");

                entity.Property(e => e.Issn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISSN");

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ruta_imagen");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.IdEditorialNavigation)
                    .WithMany(p => p.Revista)
                    .HasForeignKey(d => d.IdEditorial)
                    .HasConstraintName("FK__revista__id_edit__59FA5E80");
            });

            modelBuilder.Entity<Sancione>(entity =>
            {
                entity.HasKey(e => e.IdSancion)
                    .HasName("PK__sancione__40D35AF351AC7E34");

                entity.ToTable("sanciones");

                entity.Property(e => e.IdSancion).HasColumnName("id_sancion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.IdPenalizacion).HasColumnName("id_penalizacion");

                entity.Property(e => e.IdPrestamo).HasColumnName("id_prestamo");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Sanciones)
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("FK__sanciones__id_al__4AB81AF0");

                entity.HasOne(d => d.IdPenalizacionNavigation)
                    .WithMany(p => p.Sanciones)
                    .HasForeignKey(d => d.IdPenalizacion)
                    .HasConstraintName("FK__sanciones__id_pe__4CA06362");

                entity.HasOne(d => d.IdPrestamoNavigation)
                    .WithMany(p => p.Sanciones)
                    .HasForeignKey(d => d.IdPrestamo)
                    .HasConstraintName("FK__sanciones__id_pr__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
