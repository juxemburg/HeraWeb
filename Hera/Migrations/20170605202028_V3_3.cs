using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Migrations
{
    public partial class V3_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoDesafios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abstraccion = table.Column<int>(nullable: false),
                    Analisis = table.Column<int>(nullable: false),
                    ControlFlujo = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    DescomposicionProblemas = table.Column<int>(nullable: false),
                    Interaccion = table.Column<int>(nullable: false),
                    Paralelismo = table.Column<int>(nullable: false),
                    PensamientoAlgoritmico = table.Column<int>(nullable: false),
                    Representacion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoDesafios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoDesafios_Desafios_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoDesafios_DesafioId",
                table: "InfoDesafios",
                column: "DesafioId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoDesafios");
        }
    }
}
