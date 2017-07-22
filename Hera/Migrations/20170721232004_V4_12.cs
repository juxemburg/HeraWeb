using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V4_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Desafios_DesafioId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_DesafioId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "DesafioId",
                table: "Cursos");

            migrationBuilder.AddColumn<bool>(
                name: "Initial",
                table: "Rel_Cursos_Desafios",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Initial",
                table: "Rel_Cursos_Desafios");

            migrationBuilder.AddColumn<int>(
                name: "DesafioId",
                table: "Cursos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_DesafioId",
                table: "Cursos",
                column: "DesafioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Desafios_DesafioId",
                table: "Cursos",
                column: "DesafioId",
                principalTable: "Desafios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
