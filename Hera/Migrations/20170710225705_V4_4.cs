using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V4_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoGenerales_ResultadosScratch_ResultadoScratchId",
                table: "InfoGenerales");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoSprites_ResultadosScratch_ResultadoScratchId",
                table: "InfoSprites");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoGenerales_ResultadosScratch_ResultadoScratchId",
                table: "InfoGenerales",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoSprites_ResultadosScratch_ResultadoScratchId",
                table: "InfoSprites",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoGenerales_ResultadosScratch_ResultadoScratchId",
                table: "InfoGenerales");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoSprites_ResultadosScratch_ResultadoScratchId",
                table: "InfoSprites");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoGenerales_ResultadosScratch_ResultadoScratchId",
                table: "InfoGenerales",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoSprites_ResultadosScratch_ResultadoScratchId",
                table: "InfoSprites",
                column: "ResultadoScratchId",
                principalTable: "ResultadosScratch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
