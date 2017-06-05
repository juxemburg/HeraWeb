using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V3_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActividadesPc",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConocimientoComputador",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrecuenciaPc",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Genero",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grado",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManejoComputador",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MateriaFavorita",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActividadesPc",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "ConocimientoComputador",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "FrecuenciaPc",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "Grado",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "ManejoComputador",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "MateriaFavorita",
                table: "Estudiantes");
        }
    }
}
