using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hera.Models;
using Entities.Usuarios;
using Entities.Cursos;
using Entities.Desafios;
using Hera.Models.EntitiesViewModels;
using Entities.Calificaciones;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Tablas
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Desafio> Desafios { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<CalificacionCualitativa> CalificacionesCualitativas { get; set; }
        public DbSet<RegistroCalificacion> RegistroCalificaiones { get; set; }
        public DbSet<Rel_CursoEstudiantes> Rel_Cursos_Estudiantes { get; set; }
        public DbSet<Rel_DesafiosCursos> Rel_Cursos_Desafios { get; set; }




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasAlternateKey(e => e.UsuarioId);

            builder.Entity<ApplicationUser>()
                .Property(u => u.UsuarioId)
                .ValueGeneratedOnAdd();
            

            builder.Entity<Rel_CursoEstudiantes>()
                .HasKey(entity => 
                new { entity.CursoId, entity.EstudianteId });

            builder.Entity<Rel_DesafiosCursos>()
                .HasKey(entity => 
                new { entity.DesafioID, entity.CursoId });

            //FK_RegistroCalificacion -> Rel_CrusoEstudiantes
            builder.Entity<RegistroCalificacion>()
                .HasOne(e => e.Rel_CursoEstudiantes)
                .WithMany(rel => rel.Registros)
                .HasForeignKey(entity =>
                new { entity.CursoId, entity.EstudianteId })
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<RegistroCalificacion>()
                .HasOne(e => e.Desafio)
                .WithMany(e => e.Calificaciones)
                .HasForeignKey(e => e.DesafioId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<RegistroCalificacion>()
                .HasKey(entity =>
                new { entity.CursoId, entity.EstudianteId, entity.DesafioId });

            builder.Entity<RegistroCalificacion>()
                .HasOne(e => e.CalificacionCualitativa)
                .WithOne(e2 => e2.RegistroCalificacion)
                .HasForeignKey<CalificacionCualitativa>(entity =>
                new { entity.CursoId, entity.EstudianteId, entity.DesafioId })
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Calificacion>()
                .HasOne(e => e.RegistroCalificacion)
                .WithMany(e2 => e2.Calificaciones)
                .HasForeignKey(entity =>
                new { entity.CursoId, entity.EstudianteId, entity.DesafioId })
                .OnDelete(DeleteBehavior.SetNull); 

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        
    }
}
