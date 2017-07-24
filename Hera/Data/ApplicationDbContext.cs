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
using Entities.Valoracion;
using Entities.Notifications;

namespace Hera.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Tablas
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Desafio> Desafios { get; set; }
        public DbSet<InfoDesafio> InfoDesafios { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<CalificacionCualitativa> CalificacionesCualitativas { get; set; }
        public DbSet<RegistroCalificacion> RegistroCalificaiones { get; set; }
        public DbSet<Rel_CursoEstudiantes> Rel_Cursos_Estudiantes { get; set; }
        public DbSet<Rel_DesafiosCursos> Rel_Cursos_Desafios { get; set; }
        public DbSet<ResultadoScratch> ResultadosScratch { get; set; }
        public DbSet<BloqueScratch> BloquesScratch { get; set; }
        public DbSet<IInfoScratch_Sprite> InfoSprites { get; set; }
        public DbSet<IInfoScratch_General> InfoGenerales { get; set; }
        public DbSet<Rel_Rating> Ratings { get; set; }
        public DbSet<Notification> Notifications { get; set; }


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

            //builder.Entity<BloqueScratch>()
            //    .HasKey(e => new { e.ResultadoScratchId, e.Nombre, e.Id });

            builder.Entity<Curso>()
                .HasMany(c => c.Desafios)
                .WithOne(rel => rel.Curso)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Curso>()
                .HasMany(c => c.Estudiantes)
                .WithOne(rel => rel.Curso)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Rel_CursoEstudiantes>()
                .HasKey(entity => 
                new { entity.CursoId, entity.EstudianteId });
            builder.Entity<Rel_CursoEstudiantes>()
                .HasMany(rel => rel.Registros)
                .WithOne(reg => reg.Rel_CursoEstudiantes)
                .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Rel_CursoEstudiantes>()
            //    .HasOne(e => e.Curso)
            //    .WithMany(e2 => e2.Estudiantes)
            //    .HasForeignKey(e => e.CursoId)
            //    .OnDelete(DeleteBehavior.SetNull);
            //builder.Entity<Rel_CursoEstudiantes>()
            //    .HasOne(e => e.Estudiante)
            //    .WithMany(e2 => e2.Cursos)
            //    .HasForeignKey(e => e.EstudianteId)
            //    .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Rel_DesafiosCursos>()
                .HasKey(entity => 
                new { entity.DesafioID, entity.CursoId });
            //builder.Entity<Rel_DesafiosCursos>()
            //    .HasOne(e => e.Desafio)
            //    .WithMany(e2 => e2.Cursos)
            //    .HasForeignKey(e => e.DesafioID)
            //    .OnDelete(DeleteBehavior.SetNull);
            //builder.Entity<Rel_DesafiosCursos>()
            //    .HasOne(e => e.Curso)
            //    .WithMany(e2 => e2.Desafios)
            //    .HasForeignKey(e => e.CursoId)
            //    .OnDelete(DeleteBehavior.SetNull);

            //Registro Calificacion
            builder.Entity<RegistroCalificacion>()
                .HasOne(e => e.Rel_CursoEstudiantes)
                .WithMany(rel => rel.Registros)
                .HasForeignKey(entity =>
                new { entity.CursoId, entity.EstudianteId })
                .OnDelete(DeleteBehavior.Cascade);

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
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RegistroCalificacion>()
                .HasMany(e => e.Calificaciones)
                .WithOne(e2 => e2.RegistroCalificacion)
                .OnDelete(DeleteBehavior.Cascade);

            //Calificacion
            builder.Entity<Calificacion>()
                .HasOne(e => e.RegistroCalificacion)
                .WithMany(e2 => e2.Calificaciones)
                .HasForeignKey(entity =>
                new { entity.CursoId, entity.EstudianteId, entity.DesafioId })
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Calificacion>()
                .HasMany(e => e.Resultados)
                .WithOne(e2 => e2.Calificacion)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ResultadoScratch>()
                .HasMany(e => e.Bloques)
                .WithOne(e2 => e2.ResultadoScratch)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ResultadoScratch>()
                .HasOne(e => e.IInfoScratch_General)
                .WithOne(e2 => e2.ResultadoScratch)
                .HasForeignKey<ResultadoScratch>(e => e.IInfoScratch_GeneralId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ResultadoScratch>()
                .HasOne(e => e.IInfoScratch_Sprite)
                .WithOne(e2 => e2.ResultadoScratch)
                .HasForeignKey<ResultadoScratch>(e => e.IInfoScratch_SpriteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InfoDesafio>()
                .HasOne(e => e.Desafio)
                .WithOne(e2 => e2.InfoDesafio)
                .HasForeignKey<InfoDesafio>(e => e.DesafioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<IInfoScratch_General>()
                .HasOne(e => e.ResultadoScratch)
                .WithOne(e2 => e2.IInfoScratch_General)
                .HasForeignKey<IInfoScratch_General>
                (e => e.ResultadoScratchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<IInfoScratch_Sprite>()
                .HasOne(e => e.ResultadoScratch)
                .WithOne(e2 => e2.IInfoScratch_Sprite)
                .HasForeignKey<IInfoScratch_Sprite>
                (e => e.ResultadoScratchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Rel_Rating>()
                .HasKey(entity =>
                new { entity.ProfesorId ,entity.DesafioId });
            

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        
    }
}
