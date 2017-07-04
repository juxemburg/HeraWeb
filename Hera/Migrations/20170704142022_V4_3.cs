using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hera.Migrations
{
    public partial class V4_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstraccion",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "Analisis",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "ControlFlujo",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "DescomposicionProblemas",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "Interaccion",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "Paralelismo",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "PensamientoAlgoritmico",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "Representacion",
                table: "InfoDesafios");

            migrationBuilder.RenameColumn(
                name: "Dificultad",
                table: "Desafios",
                newName: "ProfesorId");

            migrationBuilder.AddColumn<bool>(
                name: "AdvancedEventUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BasicInputUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BasicOperators",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CloneUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ListUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MediumOperators",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MessageUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MultipleSpriteEvents",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MultipleThreads",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NestedOperators",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NonCreatedVariableUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NonUnusedBlocks",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecuenceUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SpriteSensisng",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TwoGreenFlagThread",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseMediumBlocks",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseNestedControl",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseSimpleBlocks",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UserDefinedBlocks",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VariableUse",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Desafios_ProfesorId",
                table: "Desafios",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desafios_Profesores_ProfesorId",
                table: "Desafios",
                column: "ProfesorId",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desafios_Profesores_ProfesorId",
                table: "Desafios");

            migrationBuilder.DropIndex(
                name: "IX_Desafios_ProfesorId",
                table: "Desafios");

            migrationBuilder.DropColumn(
                name: "AdvancedEventUse",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "BasicInputUse",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "BasicOperators",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "CloneUse",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "ListUse",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "MediumOperators",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "MessageUse",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "MultipleSpriteEvents",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "MultipleThreads",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "NestedOperators",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "NonCreatedVariableUse",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "NonUnusedBlocks",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "SecuenceUse",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "SpriteSensisng",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "TwoGreenFlagThread",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "UseMediumBlocks",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "UseNestedControl",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "UseSimpleBlocks",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "UserDefinedBlocks",
                table: "InfoDesafios");

            migrationBuilder.DropColumn(
                name: "VariableUse",
                table: "InfoDesafios");

            migrationBuilder.RenameColumn(
                name: "ProfesorId",
                table: "Desafios",
                newName: "Dificultad");

            migrationBuilder.AddColumn<int>(
                name: "Abstraccion",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Analisis",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ControlFlujo",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DescomposicionProblemas",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Interaccion",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Paralelismo",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PensamientoAlgoritmico",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Representacion",
                table: "InfoDesafios",
                nullable: false,
                defaultValue: 0);
        }
    }
}
