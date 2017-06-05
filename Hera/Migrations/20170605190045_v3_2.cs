using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class v3_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DirArchivo",
                table: "Desafios",
                newName: "DirSolucion");

            migrationBuilder.AddColumn<string>(
                name: "DirDesafioInicial",
                table: "Desafios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirDesafioInicial",
                table: "Desafios");

            migrationBuilder.RenameColumn(
                name: "DirSolucion",
                table: "Desafios",
                newName: "DirArchivo");
        }
    }
}
