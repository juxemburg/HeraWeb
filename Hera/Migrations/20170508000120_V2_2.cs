using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V2_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultadoScratchId",
                table: "Calificaciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ResultadoScratchId2",
                table: "Calificaciones",
                column: "ResultadoScratchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_ResultadoScratchId2",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "ResultadoScratchId",
                table: "Calificaciones");
        }
    }
}
