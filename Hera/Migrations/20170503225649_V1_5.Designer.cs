﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Hera.Data;

namespace Hera.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170503225649_V1_5")]
    partial class V1_5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Calificaciones.Calificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BloquesRepetidos");

                    b.Property<int>("CursoId");

                    b.Property<int>("DesafioId");

                    b.Property<string>("DirArchivo");

                    b.Property<int>("EstudianteId");

                    b.Property<int>("Inicializacion");

                    b.Property<DateTime?>("TiempoFinal");

                    b.Property<DateTime>("Tiempoinicio");

                    b.HasKey("Id");

                    b.HasIndex("CursoId", "EstudianteId", "DesafioId");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("Entities.Calificaciones.CalificacionCualitativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Completada");

                    b.Property<int>("CursoId");

                    b.Property<int>("DesafioId");

                    b.Property<string>("Descripcion");

                    b.Property<int>("EstudianteId");

                    b.Property<DateTime>("FechaRegistro");

                    b.HasKey("Id");

                    b.HasIndex("CursoId", "EstudianteId", "DesafioId")
                        .IsUnique();

                    b.ToTable("CalificacionesCualitativas");
                });

            modelBuilder.Entity("Entities.Calificaciones.RegistroCalificacion", b =>
                {
                    b.Property<int>("CursoId");

                    b.Property<int>("EstudianteId");

                    b.Property<int>("DesafioId");

                    b.HasKey("CursoId", "EstudianteId", "DesafioId");

                    b.HasIndex("DesafioId");

                    b.ToTable("RegistroCalificaiones");
                });

            modelBuilder.Entity("Entities.Cursos.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DesafioId");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Nombre");

                    b.Property<string>("Password");

                    b.Property<int>("ProfesorId");

                    b.HasKey("Id");

                    b.HasIndex("DesafioId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Entities.Cursos.Rel_CursoEstudiantes", b =>
                {
                    b.Property<int>("CursoId");

                    b.Property<int>("EstudianteId");

                    b.HasKey("CursoId", "EstudianteId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Rel_Cursos_Estudiantes");
                });

            modelBuilder.Entity("Entities.Desafios.Desafio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<int>("Dificultad");

                    b.Property<string>("DirArchivo");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Desafios");
                });

            modelBuilder.Entity("Entities.Desafios.Rel_DesafiosCursos", b =>
                {
                    b.Property<int>("DesafioID");

                    b.Property<int>("CursoId");

                    b.HasKey("DesafioID", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("Rel_Cursos_Desafios");
                });

            modelBuilder.Entity("Entities.Usuarios.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.HasAlternateKey("UsuarioId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Entities.Usuarios.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos");

                    b.Property<int>("Edad");

                    b.Property<string>("Nombres");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("Entities.Usuarios.Profesor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos");

                    b.Property<string>("Nombres");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Entities.Calificaciones.Calificacion", b =>
                {
                    b.HasOne("Entities.Calificaciones.RegistroCalificacion", "RegistroCalificacion")
                        .WithMany("Calificaciones")
                        .HasForeignKey("CursoId", "EstudianteId", "DesafioId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Entities.Calificaciones.CalificacionCualitativa", b =>
                {
                    b.HasOne("Entities.Calificaciones.RegistroCalificacion", "RegistroCalificacion")
                        .WithOne("CalificacionCualitativa")
                        .HasForeignKey("Entities.Calificaciones.CalificacionCualitativa", "CursoId", "EstudianteId", "DesafioId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Entities.Calificaciones.RegistroCalificacion", b =>
                {
                    b.HasOne("Entities.Desafios.Desafio", "Desafio")
                        .WithMany("Calificaciones")
                        .HasForeignKey("DesafioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Entities.Cursos.Rel_CursoEstudiantes", "Rel_CursoEstudiantes")
                        .WithMany("Registros")
                        .HasForeignKey("CursoId", "EstudianteId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Entities.Cursos.Curso", b =>
                {
                    b.HasOne("Entities.Desafios.Desafio", "Desafio")
                        .WithMany()
                        .HasForeignKey("DesafioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Usuarios.Profesor", "Profesor")
                        .WithMany("Cursos")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.Cursos.Rel_CursoEstudiantes", b =>
                {
                    b.HasOne("Entities.Cursos.Curso", "Curso")
                        .WithMany("Estudiantes")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Usuarios.Estudiante", "Estudiante")
                        .WithMany("Cursos")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.Desafios.Rel_DesafiosCursos", b =>
                {
                    b.HasOne("Entities.Cursos.Curso", "Curso")
                        .WithMany("Desafios")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Desafios.Desafio", "Desafio")
                        .WithMany()
                        .HasForeignKey("DesafioID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.Usuarios.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.Usuarios.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Usuarios.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
