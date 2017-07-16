using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V4_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalificacionesCualitativas_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas");

            migrationBuilder.AddForeignKey(
                name: "FK_CalificacionesCualitativas_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                principalTable: "RegistroCalificaiones",
                principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalificacionesCualitativas_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas");

            migrationBuilder.AddForeignKey(
                name: "FK_CalificacionesCualitativas_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                principalTable: "RegistroCalificaiones",
                principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                onDelete: ReferentialAction.SetNull);
        }
    }
}
