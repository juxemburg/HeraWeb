using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V5_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rel_Cursos_Desafios_Desafios_DesafioID",
                table: "Rel_Cursos_Desafios");

            migrationBuilder.RenameColumn(
                name: "DesafioID",
                table: "Rel_Cursos_Desafios",
                newName: "DesafioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rel_Cursos_Desafios_Desafios_DesafioId",
                table: "Rel_Cursos_Desafios",
                column: "DesafioId",
                principalTable: "Desafios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rel_Cursos_Desafios_Desafios_DesafioId",
                table: "Rel_Cursos_Desafios");

            migrationBuilder.RenameColumn(
                name: "DesafioId",
                table: "Rel_Cursos_Desafios",
                newName: "DesafioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rel_Cursos_Desafios_Desafios_DesafioID",
                table: "Rel_Cursos_Desafios",
                column: "DesafioID",
                principalTable: "Desafios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
