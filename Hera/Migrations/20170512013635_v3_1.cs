using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Migrations
{
    public partial class v3_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BloquesScratch",
                table: "BloquesScratch");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "BloquesScratch",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BloquesScratch",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloquesScratch",
                table: "BloquesScratch",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BloquesScratch_ResultadoScratchId",
                table: "BloquesScratch",
                column: "ResultadoScratchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BloquesScratch",
                table: "BloquesScratch");

            migrationBuilder.DropIndex(
                name: "IX_BloquesScratch_ResultadoScratchId",
                table: "BloquesScratch");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BloquesScratch");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "BloquesScratch",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloquesScratch",
                table: "BloquesScratch",
                columns: new[] { "ResultadoScratchId", "Nombre" });
        }
    }
}
