using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V4_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "Calificaciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "Calificaciones",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                principalTable: "RegistroCalificaiones",
                principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "Calificaciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "Calificaciones",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                principalTable: "RegistroCalificaiones",
                principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                onDelete: ReferentialAction.SetNull);
        }
    }
}
