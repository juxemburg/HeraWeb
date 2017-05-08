using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class v2_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_ResultadosScratch_ResultadoScratchId",
                table: "Calificaciones",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
