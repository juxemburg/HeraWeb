using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V7_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalificacionesCualitativas_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas");

            migrationBuilder.DropIndex(
                name: "IX_CalificacionesCualitativas_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "CalificacionesCualitativas");

            migrationBuilder.DropColumn(
                name: "DesafioId",
                table: "CalificacionesCualitativas");

            migrationBuilder.RenameColumn(
                name: "EstudianteId",
                table: "CalificacionesCualitativas",
                newName: "CalificacionId");

            migrationBuilder.AddColumn<int>(
                name: "CalificacionCualitativaId",
                table: "Calificaciones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalificacionesCualitativas_CalificacionId",
                table: "CalificacionesCualitativas",
                column: "CalificacionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalificacionesCualitativas_Calificaciones_CalificacionId",
                table: "CalificacionesCualitativas",
                column: "CalificacionId",
                principalTable: "Calificaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalificacionesCualitativas_Calificaciones_CalificacionId",
                table: "CalificacionesCualitativas");

            migrationBuilder.DropIndex(
                name: "IX_CalificacionesCualitativas_CalificacionId",
                table: "CalificacionesCualitativas");

            migrationBuilder.DropColumn(
                name: "CalificacionCualitativaId",
                table: "Calificaciones");

            migrationBuilder.RenameColumn(
                name: "CalificacionId",
                table: "CalificacionesCualitativas",
                newName: "EstudianteId");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "CalificacionesCualitativas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DesafioId",
                table: "CalificacionesCualitativas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CalificacionesCualitativas_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalificacionesCualitativas_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                principalTable: "RegistroCalificaiones",
                principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
