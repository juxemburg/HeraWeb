using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hera.Migrations
{
    public partial class V4_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IInfoScratch_GeneralId",
                table: "ResultadosScratch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IInfoScratch_SpriteId",
                table: "ResultadosScratch",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InfoGenerales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvancedEventUse = table.Column<int>(nullable: false),
                    AdvancedOperators = table.Column<int>(nullable: false),
                    BasicInputUse = table.Column<int>(nullable: false),
                    BasicOperators = table.Column<int>(nullable: false),
                    CloneUse = table.Column<int>(nullable: false),
                    EventsUse = table.Column<bool>(nullable: false),
                    ListUse = table.Column<bool>(nullable: false),
                    MediumOperators = table.Column<int>(nullable: false),
                    MessageUse = table.Column<bool>(nullable: false),
                    MultipleThreads = table.Column<int>(nullable: false),
                    NonUnusedBlocks = table.Column<int>(nullable: false),
                    ResultadoScratchId = table.Column<int>(nullable: false),
                    SecuenceUse = table.Column<int>(nullable: false),
                    SharedVariables = table.Column<bool>(nullable: false),
                    SpriteSensing = table.Column<int>(nullable: false),
                    TwoGreenFlagTrhead = table.Column<int>(nullable: false),
                    UseMediumBlocks = table.Column<int>(nullable: false),
                    UseNestedControl = table.Column<int>(nullable: false),
                    UseSimpleBlocks = table.Column<int>(nullable: false),
                    UserDefinedBlocks = table.Column<int>(nullable: false),
                    VariableCreation = table.Column<int>(nullable: false),
                    VariableUse = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoGenerales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoGenerales_ResultadosScratch_ResultadoScratchId",
                        column: x => x.ResultadoScratchId,
                        principalTable: "ResultadosScratch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoSprites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvancedEventUse = table.Column<bool>(nullable: false),
                    AdvancedOperators = table.Column<bool>(nullable: false),
                    BasicInputUse = table.Column<bool>(nullable: false),
                    BasicOperators = table.Column<bool>(nullable: false),
                    CloneUse = table.Column<bool>(nullable: false),
                    MediumOperators = table.Column<bool>(nullable: false),
                    MultipleThreads = table.Column<bool>(nullable: false),
                    NonUnusedBlocks = table.Column<bool>(nullable: false),
                    ResultadoScratchId = table.Column<int>(nullable: false),
                    SecuenceUse = table.Column<bool>(nullable: false),
                    SpriteSensing = table.Column<bool>(nullable: false),
                    TwoGreenFlagTrhead = table.Column<bool>(nullable: false),
                    UseMediumBlocks = table.Column<bool>(nullable: false),
                    UseNestedControl = table.Column<bool>(nullable: false),
                    UseSimpleBlocks = table.Column<bool>(nullable: false),
                    UserDefinedBlocks = table.Column<bool>(nullable: false),
                    VariableCreation = table.Column<bool>(nullable: false),
                    VariableUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSprites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoSprites_ResultadosScratch_ResultadoScratchId",
                        column: x => x.ResultadoScratchId,
                        principalTable: "ResultadosScratch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoGenerales_ResultadoScratchId",
                table: "InfoGenerales",
                column: "ResultadoScratchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoSprites_ResultadoScratchId",
                table: "InfoSprites",
                column: "ResultadoScratchId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoGenerales");

            migrationBuilder.DropTable(
                name: "InfoSprites");

            migrationBuilder.DropColumn(
                name: "IInfoScratch_GeneralId",
                table: "ResultadosScratch");

            migrationBuilder.DropColumn(
                name: "IInfoScratch_SpriteId",
                table: "ResultadosScratch");
        }
    }
}
