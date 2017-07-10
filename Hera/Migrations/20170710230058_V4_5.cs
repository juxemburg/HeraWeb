using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V4_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoDesafios_Desafios_DesafioId",
                table: "InfoDesafios");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoDesafios_Desafios_DesafioId",
                table: "InfoDesafios",
                column: "DesafioId",
                principalTable: "Desafios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoDesafios_Desafios_DesafioId",
                table: "InfoDesafios");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoDesafios_Desafios_DesafioId",
                table: "InfoDesafios",
                column: "DesafioId",
                principalTable: "Desafios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
