using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Migrations
{
    public partial class V2_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloquesRepetidos",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "Inicializacion",
                table: "Calificaciones");

            migrationBuilder.CreateTable(
                name: "ResultadosScratch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalificacionId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    NumBloques = table.Column<int>(nullable: false),
                    NumScripts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosScratch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadosScratch_Calificaciones_CalificacionId",
                        column: x => x.CalificacionId,
                        principalTable: "Calificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloquesScratch",
                columns: table => new
                {
                    ResultadoScratchId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Frecuencia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloquesScratch", x => new { x.ResultadoScratchId, x.Nombre });
                    table.ForeignKey(
                        name: "FK_BloquesScratch_ResultadosScratch_ResultadoScratchId",
                        column: x => x.ResultadoScratchId,
                        principalTable: "ResultadosScratch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosScratch_CalificacionId",
                table: "ResultadosScratch",
                column: "CalificacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloquesScratch");

            migrationBuilder.DropTable(
                name: "ResultadosScratch");

            migrationBuilder.AddColumn<int>(
                name: "BloquesRepetidos",
                table: "Calificaciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inicializacion",
                table: "Calificaciones",
                nullable: false,
                defaultValue: 0);
        }
    }
}
