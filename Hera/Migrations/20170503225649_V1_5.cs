using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Migrations
{
    public partial class V1_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalificacionesCualitativas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Completada = table.Column<bool>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    EstudianteId = table.Column<int>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalificacionesCualitativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalificacionesCualitativas_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                        columns: x => new { x.CursoId, x.EstudianteId, x.DesafioId },
                        principalTable: "RegistroCalificaiones",
                        principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalificacionesCualitativas_CursoId_EstudianteId_DesafioId",
                table: "CalificacionesCualitativas",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalificacionesCualitativas");
        }
    }
}
