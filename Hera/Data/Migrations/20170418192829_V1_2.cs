using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Data.Migrations
{
    public partial class V1_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Profesores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Estudiantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Cursos",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UsuarioId",
                table: "AspNetUsers",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UsuarioId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Profesores");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Cursos");
        }
    }
}
