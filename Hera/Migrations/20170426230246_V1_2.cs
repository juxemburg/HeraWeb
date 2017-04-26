using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Migrations
{
    public partial class V1_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistroCalificaiones",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroCalificaiones", x => new { x.CursoId, x.EstudianteId, x.DesafioId });
                    table.ForeignKey(
                        name: "FK_RegistroCalificaiones_Desafios_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                        columns: x => new { x.CursoId, x.EstudianteId },
                        principalTable: "Rel_Cursos_Estudiantes",
                        principalColumns: new[] { "CursoId", "EstudianteId" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BloquesRepetidos = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false),
                    Inicializacion = table.Column<int>(nullable: false),
                    TiempoFinal = table.Column<DateTime>(nullable: false),
                    Tiempoinicio = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                        columns: x => new { x.CursoId, x.EstudianteId, x.DesafioId },
                        principalTable: "RegistroCalificaiones",
                        principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_CursoId_EstudianteId_DesafioId",
                table: "Calificaciones",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroCalificaiones_DesafioId",
                table: "RegistroCalificaiones",
                column: "DesafioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "RegistroCalificaiones");
        }
    }
}
