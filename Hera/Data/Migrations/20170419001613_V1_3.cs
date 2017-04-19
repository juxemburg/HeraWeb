using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Data.Migrations
{
    public partial class V1_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dirArchivo",
                table: "Desafios",
                newName: "DirArchivo");

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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "DirArchivo",
                table: "Desafios",
                newName: "dirArchivo");
        }
    }
}
