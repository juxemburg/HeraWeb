using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V2_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones");

            migrationBuilder.AlterColumn<int>(
                name: "ResultadoScratchId",
                table: "Calificaciones",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones");

            migrationBuilder.AlterColumn<int>(
                name: "ResultadoScratchId",
                table: "Calificaciones",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
