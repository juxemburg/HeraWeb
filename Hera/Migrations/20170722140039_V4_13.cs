using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V4_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones",
                columns: new[] { "CursoId", "EstudianteId" },
                principalTable: "Rel_Cursos_Estudiantes",
                principalColumns: new[] { "CursoId", "EstudianteId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones",
                columns: new[] { "CursoId", "EstudianteId" },
                principalTable: "Rel_Cursos_Estudiantes",
                principalColumns: new[] { "CursoId", "EstudianteId" },
                onDelete: ReferentialAction.SetNull);
        }
    }
}
